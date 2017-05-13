using System;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.State;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Core.Resources
{
    public class Resource
    {
        public enum ResourceType
        {
            Item = 0,
            Container = 1
        }

        public Resource(ResourceKey resourceKey, Guid key, string text, ImageResource imageResource)
        {
            ResourceKey = resourceKey;
            Key = key;
            Text = text;
            ImageResource = imageResource;

            Populated = false;

            Type = ResourceType.Item;

            IsLeaf = false;

            State = new State<Resource>(this);
        }

        public ResourceKey ResourceKey { get; private set; }
        public ImageResource ImageResource { get; private set; }

        public Guid Key { get; private set; }
        public string Text { get; private set; }

        public ResourceType Type { get; private set; }

        public bool Populated { get; set; }

        public bool HasImage => ImageResource.Image != null;

        public bool IsLeaf { get; private set; }

        public State<Resource> State { get; private set; }

        public void Refresh()
        {
            Populated = false;
        }

        public Resource AsLeaf()
        {
            IsLeaf = true;

            return this;
        }

        public Resource AsContainer()
        {
            Type = ResourceType.Container;

            return this;
        }

        public void AssignText(string text)
        {
            Guard.AgainstNullOrEmptyString(text, "text");

            Text = text;
        }
    }
}
