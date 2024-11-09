using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSHM.Presentation.Base;
using CSHM.Presentations.Media;

namespace CSHM.Core.Handlers.Interfaces
{
    public interface IMediaHandler
    {

        MessageViewModel AddOrUpdate(MediaFileViewModel mediaFile, int creatorID);

        FileResultViewModel Download(string fileName, bool showOnlyConfirmed);

        MessageViewModel RemoveAllByEntity<T>(int entityID, int creatorID);

       
        FileResultViewModel DownloadOnce(string fileName, bool showOnlyConfirmed);
    }
}
