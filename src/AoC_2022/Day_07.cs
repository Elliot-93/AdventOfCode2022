namespace AoC_2022;

public partial class Day_07 : BaseDay
{
    private readonly DirObject _input;

    public Day_07()
    {
        _input = ParseInput();
    }

    public override ValueTask<string> Solve_1()
    {
        var allDirs = _input.GetAllDirsRecursive();
        var smallDirs = allDirs.Where(d => d.GetSize() <= 100000);

        return new(smallDirs.Sum(d => d.GetSize()).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var totalSpace = 70000000;
        var unusedRequired = 30000000;

        var allDirs = _input.GetAllDirsRecursive();
        var currentTotalUsed = _input.GetSize();
        var freeSpace = totalSpace - currentTotalUsed;
        var freeSpaceRequired = unusedRequired - freeSpace;

        var smallest = allDirs.Where(d => d.GetSize() > freeSpaceRequired).Min(d => d.GetSize());
        return new(smallest.ToString());
    }

    private DirObject ParseInput()
    {
        var allDirs = new List<DirObject>();
        var root = new DirObject("/", null);

        allDirs.Add(root);

        var currentDir = root;
        var file = new ParsedFile(InputFilePath);

        foreach (var line in file)
        {
            var e1 = line.ElementAt<string>(0);
            var e2 = line.ElementAt<string>(1);

            if (e1 == "$")
            {              
                switch (e2)
                {
                    case "cd":
                        var e3 = line.ElementAt<string>(2);
                        switch (e3)
                        {
                            case "/":
                                currentDir = root;
                                continue;
                            case "..":
                                currentDir = currentDir?.Parent;
                                continue;
                            default:
                                // may need to add here if nav before ls
                                currentDir = currentDir?
                                    .Contents.OfType<DirObject>()
                                    .First(d => d.GetName() == e3);
                                continue;
                        }
                    case "ls":
                        continue;
                    default:
                        throw new NotSupportedException($"cmd [{e2}] not supported");
                }
            }
            else
            {
                if (e1 == "dir")
                {
                    currentDir?.AddDir(e2);
                }
                else
                {
                    currentDir?.AddFile(e2, int.Parse(e1));
                }
            }
        }

        return root;
    }

    interface ITreeObject 
    {
        string GetName();
        int GetSize();
    }

    class FileObject : ITreeObject
    {
        string name;
        int size;

        public FileObject(string name, int size)
        {
            this.name = name;
            this.size = size;
        }

        public string GetName()
        {
            return name;
        }

        public int GetSize()
        {
            return size;
        }
    }

    class DirObject : ITreeObject
    {
        string name;

        public List<ITreeObject> Contents { get; set; }

        public DirObject? Parent {get; set;}


        public DirObject(string name, DirObject parent)
        {
            this.name = name;
            Contents= new List<ITreeObject>();
            Parent = parent;
        }

        public string GetName()
        {
            return name;
        }

        public int GetSize()
        {
            var sumFiles = Contents.OfType<FileObject>().Sum(f => f.GetSize());
            var sumFolders = Contents.OfType<DirObject>().Sum(f => f.GetSize());
            return sumFiles + sumFolders;
        }

        public void AddDir(string dirName)
        {
            if (!Contents.OfType<DirObject>().Any(d => d.GetName() == dirName))
            {
                Contents.Add(new DirObject(dirName, this));
            }
        }

        public void AddFile(string fileName, int fileSize)
        {
            if (!Contents.OfType<FileObject>().Any(d => d.GetName() == fileName))
            {
                Contents.Add(new FileObject(fileName, fileSize));
            }
        }

        public List<DirObject> GetAllDirsRecursive()
        {
            var allDirs = new List<DirObject>();

            foreach (var childDir in Contents.OfType<DirObject>().ToList())
            {
                allDirs.AddRange(childDir.GetAllDirsRecursive());
            }

            return Contents.OfType<DirObject>().ToList().Union(allDirs).ToList();
        }
    }
}


