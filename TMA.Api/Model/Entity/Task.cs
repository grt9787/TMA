﻿namespace TMA.Api.Model
{
    public class Tasks 
    {
        public int TaskId { get; set; }
        public int TaskTypeId { get; set; }
        public int TaskStatusId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}