﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Reservas_Final.Models.Reservas
{
    public class ApiResponse<T>
    {
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("reservas")]
        public T reservas { get; set; }
        [JsonProperty("habitaciones")]
        public T habitaciones { get; set; }
    }
}
