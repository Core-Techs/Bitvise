using System;
using System.IO;
using System.Linq;
using System.Web.Http;
using CoreTechs.Bitvise.Common;
using CoreTechs.Bitvise.WebService.Infrastructure;
using CoreTechs.Bitvise.WebService.Validation;
using JetBrains.Annotations;

namespace CoreTechs.Bitvise.WebService
{
    [RoutePrefix("bitvise")]
    public class BitviseController : ApiController
    {
        private readonly BitviseSSHServer _server;

        public BitviseController([NotNull] BitviseSSHServer server)
        {
            if (server == null) throw new ArgumentNullException("server");
            _server = server;
        }

        [Route("virtAccount/{username}")]
        public VirtAccount GetVirtAccount(string username)
        {
            return _server.GetVirtAccounts(string.Format("virtAccount eqi \"{0}\"", username)).SingleOrDefault();
        }

        [Route("virtAccount")]
        public IHttpActionResult PostVirtAccount(BitviseVirtualAccount request)
        {
            if(!this.Validate<BitviseVirtualAccountValidator>(request).IsValid)
                return BadRequest(ModelState);

            return Ok(_server.CreateOrUpdateVirtAccount(request.Username, request.NewPassword, request.Group));
        }

        [Route("~/sftp/directory/{*relativePath}")]
        public void PutSFTPDirectory(string relativePath)
        {
            var dir = Path.Combine(AppSettings.SFTPRootPath, relativePath);
            Directory.CreateDirectory(dir);
        }
    }
}