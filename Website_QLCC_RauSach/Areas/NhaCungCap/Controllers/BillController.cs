using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Website_QLCC_RauSach.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Website_QLCC_RauSach.Areas.NhaCungCap.Controllers
{
	[Area("NhaCungCap")]
	public class BillController : Controller
	{
		private readonly QuanLyRauSachContext dbContext;
		private readonly HoaDonService _donHangService;

		public BillController(QuanLyRauSachContext dbContext, HoaDonService donHangService)
		{
			this.dbContext = dbContext;
			_donHangService = donHangService;
		}

		private static IContainer CellStyle(IContainer container)
		{
			return container
				.Padding(5)
				.AlignCenter()
				.AlignMiddle();
		}

		[HttpGet]
		public IActionResult ExportBill(int id)
		{

			var donHang = _donHangService.GetDonHangInfo(id);

			if (donHang == null)
			{
				return Redirect("~/Areas/NhaCungCap/Views/Home/Index.cshtml");
			}

			var pdfDocument = Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Margin(2, Unit.Centimetre);
					page.Size(PageSizes.A4);
					page.PageColor(Color.FromRGB(255,255,255));
					page.DefaultTextStyle(x => x.FontSize(13));

					page.Header().Text("HÓA ĐƠN BÁN HÀNG").FontSize(24).Bold().AlignCenter();

					page.Content().PaddingVertical(1, Unit.Centimetre)
					.Column(column =>
					{

						column.Item().Text($"Mã hóa đơn:    {donHang.order1.MaHd}");
						column.Item().Text($"Mã đơn hàng:      {donHang.order1.MaDh}");
						column.Item().Text($"Thời gian tạo đơn:     {donHang.order1.NgayDat}");
						column.Item().Text($"Thời gian tạo hóa đơn:     {DateTime.Now.ToString("HH:mm dd-MM-yyyy")}");
						column.Item().Text($"Ngày giao hàng:     {DateTime.Now.ToString("dd-MM-yyyy")}");

						column.Item().Text($"Người đặt hàng:     {donHang.order2.TenNvst}");
						column.Item().Text($"Nhân viên bán hàng:      {donHang.order2.TenNvncc}");
						column.Item().Text($"Nhân viên giao hàng:      {donHang.NhanVienGiaoHang}");
						column.Item().Text($"Địa chỉ giao hàng:    {donHang.order1.Diachinhan}");

						column.Item().Text("Danh sách mặt hàng:").Bold();

						column.Item().Table(table =>
						{
							table.ColumnsDefinition(columns =>
							{
								columns.RelativeColumn();
								columns.RelativeColumn();
								columns.RelativeColumn();
							});

							table.Header(header =>
							{
								header.Cell().Element(CellStyle).Text("Tên mặt hàng");
								header.Cell().Element(CellStyle).Text("Số lượng");
								header.Cell().Element(CellStyle).Text("Giá");
							});

							foreach (var item in donHang.ChiTietDonHangs)
							{
								table.Cell().Element(CellStyle).Text(item.TenMh);
								table.Cell().Element(CellStyle).Text(item.SoLuongDat.ToString());
								table.Cell().Element(CellStyle).Text(item.DonGia.ToString("N0"));
							}
						});

						column.Item().Row(row =>
						{
							row.RelativeColumn().Column(col =>
							{
								col.Item().Text($"Tổng thanh toán:            {donHang.order1.Tongtien:N0}").Bold();
								col.Item().Text($"Hình thức thanh toán:     {donHang.order1.Pttt}");
								if (!donHang.order1.Ghichu.IsNullOrEmpty())
								{
									col.Item().Text($"Ghi chú:     {donHang.order1.Ghichu}");
								} else
								{
									col.Item().Text($"Ghi chú:     Không");
								}
							});
						});

						column.Item().Row(row =>
						{
							row.RelativeColumn().AlignLeft().Text("Nhân viên siêu thị (ký)");
							row.RelativeColumn().AlignRight().Text("Nhân viên giao hàng (ký)");
						});
					});
				});
			});

			var pdfStream = new MemoryStream();
			pdfDocument.GeneratePdf(pdfStream);
			pdfStream.Position = 0;

			return File(pdfStream, "application/pdf", "HoaDon.pdf");
		}
	}
}
