namespace AdapterImec.Api.Models
{
    /// <summary>
    /// Default data response model.
    /// </summary>
    /// <typeparam name="TData">The data property type.</typeparam>
    public class DatahubResponseModel<TData>
    {
        /// <summary>
        /// The data.
        /// </summary>
        public TData Data { get; set; }
    }

    /// <summary>
    /// Data response model helper.
    /// </summary>
    public static class DatahubResponseModelFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="DataResponseModel{TData}"/>.
        /// </summary>
        /// <typeparam name="TData">The data type.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static DatahubResponseModel<TData> Create<TData>(TData data)
        {
            return new DatahubResponseModel<TData>
            {
                Data = data
            };
        }
    }
}
