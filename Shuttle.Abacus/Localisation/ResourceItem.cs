/*
    This file forms part of Shuttle.Abacus.

    Shuttle.Abacus - A constraint-based calculation engine. 
    Copyright (C) 2016  Eben Roux

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Drawing;
using System.Threading;

namespace Shuttle.Abacus
{
    public class ResourceItem : IResourceAccessor
    {
        private readonly string imageKey;
        private readonly string textKey;

        public ResourceItem(string sharedKey) : this(sharedKey, sharedKey)
        {
        }

        public ResourceItem(string textKey, string imageKey)
        {
            this.textKey = textKey;
            this.imageKey = imageKey;
        }

        public string Text
        {
            get { return GetText(textKey); }
        }

        public Image Image
        {
            get { return GetImage(imageKey); }
        }

        public static string GetText(string key)
        {
            var result = Resources.ResourceManager.GetString("Text_" + key,
                                                             Thread.CurrentThread.CurrentUICulture);

            if (string.IsNullOrEmpty(result))
            {
                result = string.Format("[{0}]", key);
            }

            return result;
        }

        public static Image GetImage(string key)
        {
            return
                Resources.ResourceManager.GetObject("Image_" + key, Thread.CurrentThread.CurrentUICulture)
                as Image;
        }
    }
}
