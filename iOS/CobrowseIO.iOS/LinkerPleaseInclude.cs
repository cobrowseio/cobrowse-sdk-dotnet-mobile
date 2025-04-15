using System;

namespace Cobrowse.IO.iOS
{
    [Foundation.Preserve(AllMembers = true)]
    public class LinkerPleaseInclude
    {
        public LinkerPleaseInclude()
        {
        }

        public static void Preserve()
        {

        }

        public void Include(CobrowseIO _)
        {
            _ = new CobrowseIO();
        }

        public void Include(CobrowseIOReplayKitExtension _)
        {
            _ = new CobrowseIOReplayKitExtension();
        }
    }
}
