using System.Web;

namespace Shuvashish.Repository.UnitOfWork
{
    public class ObjectContextManager
    {
        public static NorthwindEntities GetObjectContext()
        {
            NorthwindEntities context;

            if (HttpContext.Current == null)
            {
                context = UnitOfWorkScope.CurrentObjectContext;
            }
            else
            {
                context = GetHttpContext();
            }

            return context;
        }

        private static NorthwindEntities GetHttpContext()
        {
            string ocKey = "NW_" + HttpContext.Current.GetHashCode().ToString("x");

            if (!HttpContext.Current.Items.Contains(ocKey))
            {
                var context = new NorthwindEntities();
                HttpContext.Current.Items.Add(ocKey, context);
            }

            return HttpContext.Current.Items[ocKey] as NorthwindEntities;
        }
    }
}