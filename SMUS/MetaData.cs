
using System;
using System.IO;
using TagLib;
using File = TagLib.File;

namespace SMUS
{
    //Mostly to remove path conflicts with IO.
    class MetaData : Tag, IDisposable
    {
        private readonly TagLib.File File;

        public MetaData(string _path)
        {
            File = TagLib.File.Create(_path);
            this.File.Tag.CopyTo(this, true);
        }

        [Obsolete("Use Dispose.")]
        public override void Clear() { } //Why not use dispose?

        public override TagTypes TagTypes
        { get { throw new System.NotImplementedException(); } }

        public void Dispose()
        {
            this.File.Dispose();
        }
    }
}
