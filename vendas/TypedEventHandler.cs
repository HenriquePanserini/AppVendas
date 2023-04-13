namespace vendas
{
    internal class TypedEventHandler<T1, T2>
    {
        private System.Action<DataTransferManager, DataRequestedEventArgs> shareTextHandler;

        public TypedEventHandler(System.Action<DataTransferManager, DataRequestedEventArgs> shareTextHandler)
        {
            this.shareTextHandler = shareTextHandler;
        }
    }
}