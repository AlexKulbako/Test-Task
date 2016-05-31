using System.Web.Http;
using ProjectCore.Interfaces;
using ProjectCore.Providers;

namespace TestTask.Controllers
{
    [RoutePrefix("api/filesys")]
    public class FileSystemApiController : ApiController
    {
        private readonly IFileSystemProvider _fileSys;

        public FileSystemApiController()
        {
            _fileSys = new FileSystemProvider();
        }

        [HttpGet]
        [Route("getDrives")]
        public IHttpActionResult GetDrives()
        {
            var dr = _fileSys.GetAllDrives();
            return Ok(new { code = 200, message = "Success", drives = dr });
        }
        [HttpGet]
        [Route("getSubDirectoriesByPath")]
        public IHttpActionResult GetSubDerictoriesByPath(string path)
        {
            var directoriesInfo = _fileSys.GetAllSubDirectories(path);
            return Ok(new { code = 200, message = "Success", directories = directoriesInfo });
        }
        [HttpGet]
        [Route("getFilesByPath")]
        public IHttpActionResult GetFilesByPath(string path)
        {
            var filesInfo = _fileSys.GetAllFiles(path);
            var count = _fileSys.GetFileCount(path);
            return Ok(new { code = 200, message = "Success", files = filesInfo, fileCount = count });
        }

    }
}
