namespace Shuvashish.Repository.UnitOfWork
{
    public sealed class ContextManager
    {
        private NorthwindEntities _context;

        #region Thread-safe, lazy Singleton

        /// <summary>
        /// This is a thread-safe, lazy singleton.
        /// See http://www.yoda.arachsys.com/csharp/singleton.html
        /// for more details about its implementation.
        /// </summary>
        public static ContextManager Instance
        {
            get { return Nested.ContextManager; }
        }

        /// <summary>
        /// Initializes NorthwindEFEntities.
        /// </summary>
        private ContextManager()
        {
            InitDB();
        }

        /// <summary>
        /// Assists with ensuring thread-safe, lazy singleton
        /// </summary>
        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly ContextManager ContextManager = new ContextManager();
        }

        #endregion

        private void InitDB()
        {
            _context = new NorthwindEntities();
        }

        public NorthwindEntities GetContext()
        {
            return _context;
        }
    }
}