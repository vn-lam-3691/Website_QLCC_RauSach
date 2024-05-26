1. Clone dự án về máy
2. Chạy file SQL trong thư mục Database
3. Sửa đường dẫn kết nối trong file appsettings.json:
   ```json
   "ConnectionStrings": {
      "DefaultConnection": "Data Source=<your_servername>;Initial Catalog=QuanLyRauSach;User ID=<account_name>;Password=<your_password>;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"
    }
  ```
