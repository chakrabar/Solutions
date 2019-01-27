using System;
using System.Collections.Generic;
using WorkflowContainer.Activities;
using Xunit;

namespace WorkflowContainer.Tests.Activities
{
    public class JsonUtilsTests
    {
        [Fact]
        public void Can_handle_null()
        {
            ApprovalData obj = null;

            var result = JsonUtils.Cast<ApprovalData>(obj);

            Assert.Null(result);
        }

        [Fact]
        public void Can_directly_cast_proper_object()
        {
            var approvalData = new ApprovalData
            {
                IsApproved = false,
                Message = "Fake claim"
            };

            CastAndVerifyResult(approvalData, approvalData.Message);
        }

        [Fact]
        public void Can_cast_matching_object()
        {
            var approvalData = new
            {
                IsApproved = false,
                Message = "Fake claim"
            };

            CastAndVerifyResult(approvalData, approvalData.Message);
        }

        [Fact]
        public void Can_cast_single_quoted_json_string()
        {
            var approvalData = "{'IsApproved':true,'Message':'Good claim'}";

            CastAndVerifyResult(approvalData, "Good claim");
        }

        [Fact]
        public void Can_cast_double_quoted_json_string()
        {
            var approvalData = "{\"IsApproved\":true,\"Message\":\"Good claim\"}";

            CastAndVerifyResult(approvalData, "Good claim");
        }

        [Fact]
        public void Can_cast_javascript_object_json_string()
        {
            var approvalData = "{IsApproved:true,Message:'Good claim'}";

            CastAndVerifyResult(approvalData, "Good claim");
        }

        [Fact]
        public void Can_cast_json_string_with_extra_quotes()
        {
            var approvalData = @"""{\""IsApproved\"":true,\""Message\"":\""Good claim\""}""";

            CastAndVerifyResult(approvalData, "Good claim");
        }

        [Fact]
        public void Can_NOT_cast_json_with_non_matching_quotes_1()
        {
            var approvalData = "{\"IsApproved\":true,\"Message':\"Good claim\"}";

            CastAndVerifyException(approvalData);
        }

        [Fact]
        public void Can_NOT_cast_json_with_non_matching_quotes_2()
        {
            var approvalData = @"""{\""IsApproved\"""":true,\""Message\"":\""Good claim\""}""";

            CastAndVerifyException(approvalData);
        }

        [Fact]
        public void Can_cast_string()
        {
            var approvalData = "This is a cool message";

            var result = JsonUtils.Cast<string>(approvalData);

            Assert.NotNull(result);
            Assert.Equal(approvalData, result);
        }

        [Fact]
        public void Can_cast_quoted_string_with_special_chars()
        {
            var approvalData = "Hi \"sis\", what's up? How are kid #1 & kid #2?!!";

            var result = JsonUtils.Cast<string>(approvalData);

            Assert.NotNull(result);
            Assert.Equal(approvalData, result);
        }

        [Fact]
        public void Can_cast_generic_dictionary()
        {
            var data = new Dictionary<string, object>
            {
                { "Name", "ac1" },
                { "Message", new ApprovalData
                    {
                        IsApproved = false,
                        Message = "Humans are not cool"
                    }
                }
            };

            var result = JsonUtils.Cast<IDictionary<string, object>>(data);

            Assert.NotNull(result);
            Assert.IsType<ApprovalData>(result["Message"]);
            Assert.Equal((result["Message"] as ApprovalData).Message, "Humans are not cool");
        }

        [Fact]
        public void Can_cast_generic_dictionary_in_json_object_format()
        {
            var data = "{\"Name\":\"ac1\",\"Message\":{\"IsApproved\":false,\"Message\":\"Humans are not cool\"}}";

            var result = JsonUtils.Cast<IDictionary<string, object>>(data);

            Assert.NotNull(result);
            Assert.NotNull(JsonUtils.Cast<ApprovalData>(result["Message"]));
            Assert.Equal(JsonUtils.Cast<ApprovalData>(result["Message"]).Message, "Humans are not cool");
        }

        [Fact]
        public void Can_cast_object_with_dictionary_property()
        {
            var obj = new InvoiceData
            {
                IsApproved = true,
                Messages = new Dictionary<string, object>
                {
                    { "Name", "ac1" },
                    { "Message", new ApprovalData
                        {
                            IsApproved = false,
                            Message = "Humans are not cool"
                        }
                    }
                }
            };

            var result = JsonUtils.Cast<InvoiceData>(obj);

            Assert.NotNull(result);
            Assert.IsType<ApprovalData>(result.Messages["Message"]);
            Assert.Equal((result.Messages["Message"] as ApprovalData).Message, "Humans are not cool");
        }

        [Fact]
        public void Can_cast_object_with_dictionary_property_from_json()
        {
            var obj = "{\"IsApproved\":true,\"Messages\":{\"Name\":\"ac1\",\"Message\":{\"IsApproved\":false,\"Message\":\"Humans are not cool\"}}}";

            var result = JsonUtils.Cast<InvoiceData>(obj);

            Assert.NotNull(result);
            var innerDictionaryType = result.Messages["Message"].GetType().FullName; //Newtonsoft.Json.Linq.JObject
            //Assert.IsType<ApprovalData>(result.Messages["Message"]); obviously it does not know the type
            Assert.NotNull(JsonUtils.Cast<ApprovalData>(result.Messages["Message"]));
            //Assert.Equal((result.Messages["Message"] as ApprovalData).Message, "Humans are not cool"); doesn't work for same reason
            Assert.Equal(JsonUtils.Cast<ApprovalData>(result.Messages["Message"]).Message, "Humans are not cool");
        }

        void CastAndVerifyResult(object obj, string expectedMessage)
        {
            var result = JsonUtils.Cast<ApprovalData>(obj);

            Assert.NotNull(result);
            Assert.Equal(result.Message, expectedMessage);
        }

        void CastAndVerifyException(object obj)
        {
            Assert.ThrowsAny<Exception>(() => JsonUtils.Cast<ApprovalData>(obj));
        }
    }

    class ApprovalData
    {
        public bool IsApproved { get; set; }
        public string Message { get; set; }
    }

    class InvoiceData
    {
        public bool IsApproved { get; set; }
        public IDictionary<string, object> Messages { get; set; }
    }
}
