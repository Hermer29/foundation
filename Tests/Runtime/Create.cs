using System.Collections.Generic;
using System.Linq;
using Hermer29.Foundation;

namespace Tests.Runtime
{
    public static class Create
    {
        public static Save CreateSave(IEnumerable<ISourceAdapter> sourceAdapters)
        {
            return Save.Create(sourceAdapters.ToArray());
        }
    }
}