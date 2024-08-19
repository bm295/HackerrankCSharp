using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

class Program
{
    static async Task Main(string[] args)
    {
        // URL của trang web
        string url = "https://app.haugiang.gov.vn/LichLamViec/Lich/DonVi?MaDonVi=vptu";

        // Sử dụng HttpClient để gửi yêu cầu GET
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Gửi yêu cầu GET
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // Lấy nội dung phản hồi dưới dạng chuỗi
                string pageContent = await response.Content.ReadAsStringAsync();

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

                    // Tìm vị trí của nội dung cuối cùng chứa "19/8/2024"
                    int fromDateIndex = -1;
                    for (int i = 0; i < pContents.Count; i++)
                    {
                        if (pContents[i].Contains("19/8/2024"))
                        {
                            fromDateIndex = i;
                        }
                    }

                    // Kiểm tra nếu tìm thấy "FromDate"
                    if (fromDateIndex != -1)
                    {
                        Console.WriteLine("Chương trình làm việc của Thường trực Tỉnh ủy Hậu Giang hôm nay:");

                        // In các thành phần từ "FromDate" và dừng khi gặp ", ng&agrave;y 20/8/2024"
                        for (int i = fromDateIndex; i < pContents.Count; i++)
                        {
                            // Giải mã nội dung HTML
                            string decodedContent = WebUtility.HtmlDecode(pContents[i]);

                            // Dừng khi gặp ", ng&agrave;y 20/8/2024"
                            if (decodedContent.Contains(", ngày 20/8/2024"))
                            {
                                break;
                            }

                            Console.WriteLine(decodedContent);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy thẻ <p> chứa '19/8/2024'.");
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
