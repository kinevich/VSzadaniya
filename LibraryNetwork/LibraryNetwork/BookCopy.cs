using System;

namespace LibraryNetwork
{
    class BookCopy
    {
        public Guid Id { get; }

        public Guid BookId { get; }

        public Guid EditionId { get; }

        public Guid ReleaseFormId { get; }

        public BookCopy(Book book, Edition edition, ReleaseForm releaseForm)
        {
            Id = Guid.NewGuid();
            BookId = book.Id;
            EditionId = edition.Id;
            ReleaseFormId = releaseForm.Id;
        }

    }
}
