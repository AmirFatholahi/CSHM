using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.File
{
    public abstract class FileTypeMatcher
    {
        public bool Matches(System.IO.Stream stream, bool resetPosition = true)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (!stream.CanRead || (stream.Position != 0 && !stream.CanSeek))
            {
                throw new ArgumentException("File contents must be a readable stream", nameof(stream));
            }
            if (stream.Position != 0 && resetPosition)
            {
                stream.Position = 0;
            }

            return MatchesPrivate(stream);
        }

        protected abstract bool MatchesPrivate(System.IO.Stream stream);
    }

    public class ExactFileTypeMatcher : FileTypeMatcher
    {
        private readonly byte[] _bytes;

        public ExactFileTypeMatcher(IEnumerable<byte> bytes)
        {
            _bytes = bytes.ToArray();
        }

        protected override bool MatchesPrivate(System.IO.Stream stream)
        {
            foreach (var b in _bytes)
            {
                var rb = stream.ReadByte();
                if (rb != b)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class FuzzyFileTypeMatcher : FileTypeMatcher
    {
        private readonly byte?[] _bytes;

        public FuzzyFileTypeMatcher(IEnumerable<byte?> bytes)
        {
            _bytes = bytes.ToArray();
        }
        public FuzzyFileTypeMatcher(byte[] bytes)
        {
            _bytes = bytes.Select(t => (byte?)t).ToArray();
        }
        protected override bool MatchesPrivate(System.IO.Stream stream)
        {

            foreach (var b in _bytes)
            {
                var c = stream.ReadByte();
                if (c == -1 || (b.HasValue && b.Value == 0 && c != b.Value))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class RangeFileTypeMatcher : FileTypeMatcher
    {
        private readonly FileTypeMatcher _matcher;

        private readonly int _maximumStartLocation;

        public RangeFileTypeMatcher(FileTypeMatcher matcher, int maximumStartLocation)
        {
            _matcher = matcher;
            _maximumStartLocation = maximumStartLocation;
        }

        protected override bool MatchesPrivate(System.IO.Stream stream)
        {
            for (var i = 0; i < _maximumStartLocation; i++)
            {
                if (i <= stream.Length)
                {
                    stream.Position = i;
                    if (_matcher.Matches(stream, resetPosition: false))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    public class FileType
    {
        private readonly FileTypeMatcher _fileTypeMatcher;

        public string Name { get; }

        public string Extension { get; }

        public static FileType Unknown { get; } = new FileType("unknown", string.Empty, null);

        public FileType(string name, string extension, FileTypeMatcher matcher)
        {
            Name = name;
            Extension = extension;
            _fileTypeMatcher = matcher;
        }

        public bool Matches(System.IO.Stream stream)
        {
            return _fileTypeMatcher == null || _fileTypeMatcher.Matches(stream);
        }
    }

    public class FileTypeChecker
    {
        private readonly IList<FileType> _knownFileTypes = new List<FileType>
        {

            //new FileType("Bitmap", ".bmp", new ExactFileTypeMatcher(new byte[] {0x42, 0x4d})),
            //new FileType("Portable Network Graphic", ".png", new ExactFileTypeMatcher(new byte[] {0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A})),
            //new FileType("JPEG", ".jpg", new FuzzyFileTypeMatcher(new byte?[] {0xFF, 0xD8, 0xFF, 0xE1, null, null, 0x45, 0x78, 0x69, 0x66, 0x00, 0x00})),
            //new FileType("JPEG", ".jpg", new FuzzyFileTypeMatcher(new byte?[] {0xFF, 0xD8, 0xFF, 0xDB})),
            //new FileType("JPEG", ".jpg", new FuzzyFileTypeMatcher(new byte?[] {0xFF, 0xD8, 0xFF, 0xEE})),
            //new FileType("JPEG", ".jpg", new FuzzyFileTypeMatcher(new byte?[] {0xFF, 0xD8, 0xFF, 0xE0, 0x00, 0x10, 0x4A, 0x46, 0x49, 0x46, 0x00, 0x01})),
            ////new FileType("JPEG", ".jpg", new FuzzyFileTypeMatcher(new byte?[] {0xFF, 0xD, 0xFF, 0xE0, null, null, 0x4A, 0x46, 0x49, 0x46, 0x00})),
            ////new FileType("Digital camera JPG", ".jpg",new FuzzyFileTypeMatcher(new byte?[] {0xFF, 0xD, 0xFF, 0xE1, null, null, 0x45, 0x78, 0x69, 0x66, 0x00})),
            ////new FileType("Still Picture Interchange File Format ", ".jpg", new FuzzyFileTypeMatcher(new byte?[] {0xFF, 0xD, 0xFF, 0xE8, null, null, 0x53, 0x50, 0x49, 0x46, 0x46, 0x00 })),
            //new FileType("Graphics Interchange Format 87a", ".gif", new ExactFileTypeMatcher(new byte[] {0x47, 0x49, 0x46, 0x38, 0x37, 0x61})),
            //new FileType("Graphics Interchange Format 89a", ".gif", new ExactFileTypeMatcher(new byte[] {0x47, 0x49, 0x46, 0x38, 0x39, 0x61})),
            //new FileType("Microsoft Excel", ".xlsx", new ExactFileTypeMatcher(new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06 , 0x00 })),
            //new FileType("Microsoft Word", ".docx", new ExactFileTypeMatcher(new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06 , 0x00 })),
            //new FileType("Microsoft Excel", ".xls", new ExactFileTypeMatcher(new byte[] { 0x09, 0x08, 0x10, 0x00, 0x00, 0x06, 0x05 , 0x00 })),
            //new FileType("Portable Document Format", ".pdf", new RangeFileTypeMatcher(new ExactFileTypeMatcher(new byte[] { 0x25, 0x50, 0x44, 0x46 }), 1019))
            // ... Potentially more in future
        };

        public FileType GetFileType(System.IO.Stream fileContent, List<ExtensionTypeViewModel> extensions)
        {
            foreach (var e in extensions)
            {
                if (e.MatcherType.ToUpper() == "FUZZY")
                {
                    _knownFileTypes.Add(new FileType(e.ExtensionName, e.Postfix, new FuzzyFileTypeMatcher(e.Matcher)));
                }
                else if (e.MatcherType.ToUpper() == "EXACT")
                {
                    _knownFileTypes.Add(new FileType(e.ExtensionName, e.Postfix, new ExactFileTypeMatcher(e.Matcher)));
                }
                else if (e.MatcherType.ToUpper() == "RANGE")
                {
                    _knownFileTypes.Add(new FileType(e.ExtensionName, e.Postfix, new RangeFileTypeMatcher(new ExactFileTypeMatcher(e.Matcher), 1019)));
                }

            }
            return GetFileTypes(fileContent).FirstOrDefault() ?? FileType.Unknown;
        }

        public IEnumerable<FileType> GetFileTypes(System.IO.Stream stream)
        {
            return _knownFileTypes.Where(fileType => fileType.Matches(stream));
        }
    }
}
