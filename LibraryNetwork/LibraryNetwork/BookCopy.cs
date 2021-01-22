using System;

namespace LibraryNetwork
{
    class BookCopy
    {
        public Guid Id { get; }

        public Guid BookId { get; }

        public Guid EditionId { get; }

        public Guid ReleaseFormId { get; }

        public BookCopy(Guid bookId, Guid editionId, Guid releaseFormId)
        {
            Id = Guid.NewGuid();
            BookId = bookId;
            EditionId = editionId;
            ReleaseFormId = releaseFormId;
        }

    }
}
