using System;
using System.Collections.Generic;
using System.Linq;
using Sample.Domain;
using Sample.Events;
using Sample.EventStorage;

namespace Sample.Specs
{
    [When]
    public class signing_an_unsigned_note : AggregateRootTestFixture<UnsignedNote>
    {
        readonly Signature _signature = new Signature("sig");
        SignedNote _signedNote;

        protected override IEnumerable<IEvent> Given()
        {
            return No.Changes;
        }

        protected override void When()
        {
            _signedNote = Sut.Sign(_signature);
        }

        [Then]
        public void it_returns_a_signed_note()
        {
            _signedNote.ShouldNotBeNull();
        }

        [Then]
        public void it_publishes_a_note_signed_event()
        {
            Events.First().ShouldBeInstanceOfType<NoteSignedEvent>();
        }

        [Then]
        public void the_published_event_holds_the_id_of_the_aggregate_root()
        {
            Events.First().As<NoteSignedEvent>().AggregateId.ShouldBe(Sut.Id);
        }

        [Then]
        public void the_published_event_holds_the_signature()
        {
            Events.First().As<NoteSignedEvent>().Signature.ShouldBe(_signature);
        }
    }

    [When]
    public class changing_principal_user_on_an_unsigned_note : AggregateRootTestFixture<UnsignedNote>
    {
        readonly Signature _signature = new Signature("sig");

        protected override IEnumerable<IEvent> Given()
        {
            return No.Changes;
        }

        protected override void When()
        {
            Sut.ChangePrincipalUser(_signature);
        }

        [Then]
        public void it_publishes_a_note_signed_event()
        {
            Events.First().ShouldBeInstanceOfType<PrinciaplUserChangedEvent>();
        }

        [Then]
        public void the_published_event_holds_the_id_of_the_aggregate_root()
        {
            Events.First().As<PrinciaplUserChangedEvent>().AggregateId.ShouldBe(Sut.Id);
        }

        [Then]
        public void the_published_event_holds_the_signature_of_the_new_principal_user()
        {
            Events.First().As<PrinciaplUserChangedEvent>().Signature.ShouldBe(_signature);
        }
    }

    public class PrinciaplUserChangedEvent : AggregateRootEvent
    {
        public string Signature { get; set; }

        public PrinciaplUserChangedEvent(Guid aggregateId, string signature) : base(aggregateId)
        {
            Signature = signature;
        }
    }

    public class NoteSignedEvent : AggregateRootEvent
    {
        public string Signature { get; set; }

        public NoteSignedEvent(Guid aggregateId, string signature) 
            : base(aggregateId)
        {
            Signature = signature;
        }
    }

    public class Signature
    {
        readonly string _signature;

        public Signature(string signature)
        {
            _signature = signature;
        }

        public static implicit operator string(Signature signature)
        {
            return signature._signature;
        }
    }

    public class UnsignedNote : AggregateRoot
    {
        Signature _princiaplUserSignature;

        public UnsignedNote()
            :base(Guid.NewGuid())
        {
            RegisterHandler<NoteSignedEvent>(ApplyNoteSignedEvent);
            RegisterHandler<PrinciaplUserChangedEvent>(ApplyPrinciaplUserChangedEvent);
        }

        void ApplyPrinciaplUserChangedEvent(PrinciaplUserChangedEvent e)
        {
            _princiaplUserSignature = new Signature(e.Signature);
        }

        void ApplyNoteSignedEvent(NoteSignedEvent e)
        {
        }

        public SignedNote Sign(Signature signature)
        {
            ApplyEvent(new NoteSignedEvent(Id, signature));
            return new SignedNote(signature);
        }

        public void ChangePrincipalUser(Signature newPrinciaplUserSignature)
        {
            ApplyEvent(new PrinciaplUserChangedEvent(Id, newPrinciaplUserSignature));
        }
    }

    public class SignedNote : AggregateRoot
    {
        public SignedNote()
            :base(Guid.NewGuid())
        {
        }

        public SignedNote(Signature signature) : this()
        {
            
        }
    }
}