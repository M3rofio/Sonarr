using System.IO;
using System.Threading;
using NLog;
using NzbDrone.Common.Disk;
using NzbDrone.Common.EnvironmentInfo;
using NzbDrone.Common.Processes;

namespace NzbDrone.Host
{
    public interface IWaitForExit
    {
        void Spin();
    }

    public class SpinService : IWaitForExit
    {
        private readonly IRuntimeInfo _runtimeInfo;
        private readonly IProcessProvider _processProvider;
        private readonly IDiskProvider _diskProvider;
        private readonly IStartupContext _startupContext;
        private readonly Logger _logger;

        public SpinService(IRuntimeInfo runtimeInfo, IProcessProvider processProvider, IDiskProvider diskProvider, IStartupContext startupContext, Logger logger)
        {
            _runtimeInfo = runtimeInfo;
            _processProvider = processProvider;
            _diskProvider = diskProvider;
            _startupContext = startupContext;
            _logger = logger;
        }

        public void Spin()
        {
            while (!_runtimeInfo.IsExiting)
            {
                Thread.Sleep(1000);
            }

            _logger.Debug("Wait loop was terminated.");

            if (_runtimeInfo.RestartPending)
            {
                var restartArgs = GetRestartArgs();

                var path = _runtimeInfo.ExecutingApplication;
                var installationFolder = Path.GetDirectoryName(path);

                _logger.Info("Attempting restart with arguments: {0} {1}", path, restartArgs);

                if (OsInfo.IsOsx && installationFolder.EndsWith("/bin"))
                {
                    // New MacOS App stores Sonarr binaries in Resources/bin and has a shim in MacOS
                    // Run the shim instead
                    var shim = Path.Combine(installationFolder, "../../MacOS/Sonarr");
                    if (_diskProvider.FileExists(shim))
                    {
                        path = Path.GetFullPath(shim);
                    }
                }
                
                _processProvider.SpawnNewProcess(path, restartArgs);
            }
        }

        private string GetRestartArgs()
        {
            var args = _startupContext.PreservedArguments;

            args += " /restart";

            if (!args.Contains("/nobrowser"))
            {
                args += " /nobrowser";
            }

            return args;
        }
    }
}
