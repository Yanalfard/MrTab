using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MrTab.ViewComponents
{
    [ViewComponent]
    public class CroppieViewComponent : ViewComponent
    {
        public class MyClass
        {
            public int Width { get; set; }
            public int Height { get; set; }

            public MyClass(int width, int height)
            {
                Width = width;
                Height = height;
            }
        }

        public async Task<IViewComponentResult> InvokeAsync(int width, int height)
        {
            MyClass a = new MyClass(width, height);
            return await Task.FromResult((IViewComponentResult)View("Default", a));
        }

    }
}
