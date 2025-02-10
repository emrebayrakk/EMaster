namespace EMaster.Domain.Responses
{
    public class PaginatedResponse<T>
    {
        public int Code { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public int TotalCount { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 5;
        public T Data { get; set; }
     
        public PaginatedResponse(
            bool status,
            int code,
            string message,
            int totalCount,
            int pageNumber = 1,
            int pageSize = 5,
            T data = default)
        {
            Code = code;
            Status = status;
            Message = message; TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
        }    
    }
}

