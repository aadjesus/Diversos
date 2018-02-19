using System;
using Mogre;

namespace OgreLib
{
    public partial class OgreImage
    {
        private double _resourceItemScalar;
        private double _currentProcess;

        protected virtual void CallResourceItemLoaded(ResourceLoadEventArgs e)
        {
            Dispatcher.Invoke((MethodInvoker)(() => OnResourceItemLoaded(e)));
        }

        protected virtual void OnResourceItemLoaded(ResourceLoadEventArgs e)
        {
            if (ResourceLoadItemProgress != null) ResourceLoadItemProgress(this, e);
        }

        private void InitResourceLoad()
        {
            ResourceGroupManager.Singleton.ResourceGroupLoadStarted += Singleton_ResourceGroupLoadStarted;
            ResourceGroupManager.Singleton.ResourceGroupScriptingStarted += Singleton_ResourceGroupScriptingStarted;
            ResourceGroupManager.Singleton.ScriptParseStarted += Singleton_ScriptParseStarted;
            ResourceGroupManager.Singleton.ResourceLoadStarted += Singleton_ResourceLoadStarted;
            ResourceGroupManager.Singleton.WorldGeometryStageStarted += Singleton_WorldGeometryStageStarted;

            _currentProcess = 0;
        }

        private void Singleton_WorldGeometryStageStarted(string description)
        {
            _currentProcess += _resourceItemScalar;
            CallResourceItemLoaded(new ResourceLoadEventArgs(description, _currentProcess));
        }

        private void Singleton_ResourceLoadStarted(ResourcePtr resource)
        {
            _currentProcess += _resourceItemScalar;
            CallResourceItemLoaded(new ResourceLoadEventArgs(resource.Name, _currentProcess));
        }

        private void Singleton_ScriptParseStarted(string scriptName)
        {
            _currentProcess += _resourceItemScalar;
            CallResourceItemLoaded(new ResourceLoadEventArgs(scriptName, _currentProcess));
        }

        private void Singleton_ResourceGroupScriptingStarted(string groupName, uint scriptCount)
        {
            _resourceItemScalar = (scriptCount > 0)
                                      ? 0.4d / scriptCount
                                      : 0;
        }

        private void Singleton_ResourceGroupLoadStarted(string groupName, uint resourceCount)
        {
            _resourceItemScalar = (resourceCount > 0)
                                      ? 0.6d / resourceCount
                                      : 0;
        }

        public event EventHandler<ResourceLoadEventArgs> ResourceLoadItemProgress;
    }

    public class ResourceLoadEventArgs : EventArgs
    {
        public ResourceLoadEventArgs(string name, double progress)
        {
            this.Name = name;
            this.Progress = progress;
        }

        public string Name { get; private set; }
        public double Progress { get; private set; }
    }
}