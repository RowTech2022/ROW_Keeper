using System;

namespace Keeper.Client
{
    public class BaseDto
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        
        public byte[] Timestamp { get; set; } = null!;
    }
}