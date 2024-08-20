// @nuget: HtmlAgilityPack
// @nuget: System.Net.Http
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using HtmlAgilityPack;
					
public class Program
{
	public static void Main()
	{
		// URL của trang web
        string url = "https://app.haugiang.gov.vn/LichLamViec/Lich/DonVi?MaDonVi=vptu";

        // Sử dụng HttpClient để gửi yêu cầu GET
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Gửi yêu cầu GET
                HttpResponseMessage response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();

                // Lấy nội dung phản hồi dưới dạng chuỗi
                string pageContent = response.Content.ReadAsStringAsync().Result;

                // Tải nội dung vào HtmlDocument (sử dụng HtmlAgilityPack)
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(pageContent);

                // Chọn tất cả các thẻ <p>
                var pNodes = document.DocumentNode.SelectNodes("//p");

                if (pNodes != null)
                {
                    // Tạo danh sách để lưu nội dung các thẻ <p>
                    List<string> pContents = new List<string>();

                    // Đưa nội dung của từng thẻ <p> vào danh sách
                    foreach (var p in pNodes)
                    {
                        pContents.Add(p.InnerText.Trim());
                    }

                    // Tìm vị trí của nội dung cuối cùng chứa fromDate
					var fromDate = "20/8/2024";
					var toDate = ", ngày 21/8/2024";
                    int fromDateIndex = -1;
                    for (int i = 0; i < pContents.Count; i++)
                    {
                        if (pContents[i].Contains(fromDate))
                        {
                            fromDateIndex = i;
                        }
                    }

                    // Kiểm tra nếu tìm thấy "fromDate"
                    if (fromDateIndex != -1)
                    {
                        Console.WriteLine("Chương trình làm việc của Thường trực Tỉnh ủy Hậu Giang hôm nay:");

                        // In các thành phần từ "FromDate" và dừng khi gặp "toDate"
                        for (int i = fromDateIndex; i < pContents.Count; i++)
                        {
                            // Giải mã nội dung HTML
                            string decodedContent = WebUtility.HtmlDecode(pContents[i]);

                            // Dừng khi gặp toDate
                            if (decodedContent.Contains(toDate))
                            {
                                break;
                            }

                            Console.WriteLine(decodedContent);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy thẻ <p> chứa fromDate'.");
                    }
                }
                else
                {
                    Console.WriteLine("Không tìm thấy thẻ <p> nào trên trang.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
            }
        }
	}
}
