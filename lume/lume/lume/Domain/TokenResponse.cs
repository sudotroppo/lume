using System;
using System.Collections.Generic;
using System.Text;

namespace lume.Domain
{
    class TokenResponse
    {
        public bool Success { get; set; }
        public Utente Utente { get; set; }
        public string Token { get; set; }

        public override string ToString()
        {
            return Token;
        }
    }
}
