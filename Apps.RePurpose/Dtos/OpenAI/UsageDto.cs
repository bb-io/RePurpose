﻿using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.RePurpose.Dtos.OpenAI
{
    public class UsageDto
    {
        [Display("Prompt tokens")]
        [JsonProperty("prompt_tokens")]
        public int PromptTokens { get; set; }

        [Display("Completion tokens")]
        [JsonProperty("completion_tokens")]
        public int CompletionTokens { get; set; }

        [Display("Total tokens")]
        [JsonProperty("total_tokens")]
        public int TotalTokens { get; set; }

        public static UsageDto operator +(UsageDto u1, UsageDto u2)
        {
            return new UsageDto
            {
                PromptTokens = u1.PromptTokens + u2.PromptTokens,
                CompletionTokens = u1.CompletionTokens + u2.CompletionTokens,
                TotalTokens = u1.TotalTokens + u2.TotalTokens,
            };
        }
    }
}
