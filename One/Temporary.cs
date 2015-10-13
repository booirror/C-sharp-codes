using System;
using System.IO;
class Temporary : IDisposable
{
    public Temporary()
    {
        _File = new FileInfo(Path.GetTempFileName());
        _Stream = new FileStream(File.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
    }

    ~Temporary()
    {
        Dispose(false);
    }

    public FileStream Stream
    {
        get
        {
            return _Stream;
        }
    }
    readonly private FileStream _Stream;

    public FileInfo File
    {
        get { return _File; }
    }
    readonly private FileInfo _File;

    #region IDispose Members
    public void Dispose()
    {
        Dispose(true);
        System.GC.SuppressFinalize(this);
    }
    #endregion

    public void Dispose(bool dispoing)
    {
        if (dispoing)
        {
            if (Stream != null)
            {
                Stream.Close();
            }
        }
        if (File != null)
        {
            File.Delete();
        }
    }
}