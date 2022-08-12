using AdapterImec.Domain.Exceptions;

namespace AdapterImec.Application.Extensions
{
    public static class ObjectExtension
    {
        public static void IsNotFound(this object obj)
        {
            if (obj == null)
            {
                throw new NotFoundException();
            }
        }
    }
}
