﻿using NextGenSoftware.OASIS.API.Providers.SOLANAOASIS.Entities.DTOs.Common;

namespace NextGenSoftware.OASIS.API.Providers.SOLANAOASIS.Infrastructure.Entities.DTOs.Responses
{
    public class SendNftResult : BaseTransactionResult
    {
        public SendNftResult(string transactionHash) : base(transactionHash)
        {
        }

        public SendNftResult()
        {
            
        }
    }
}