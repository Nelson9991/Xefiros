using System;

namespace Xefiros.Shared.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}