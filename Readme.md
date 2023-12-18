# otel_dotnet_lambda_sample

## 專案描述
此專案是一個基本的 .NET 應用，用於展示如何透過 AWS Lambda 和 Amazon X-Ray 服務，使用 OpenTelemetry 來追蹤和監控應用性能。

## 功能
- **資料上傳到 X-Ray**: 這個範例展示了如何將追蹤資料從 .NET 應用上傳到 Amazon X-Ray 服務。
- **OpenTelemetry 集成**: 利用 OpenTelemetry 來追蹤應用中的請求和性能指標。
  
## 使用方法
1. 克隆此存儲庫到本地。
2. 依據 `aws-lambda-tools-defaults.json` 配置您的 AWS Lambda 函數。
3. 使用 `dotnet lambda` 命令部署函數到 AWS Lambda。

## Lambda 主動追蹤
要啟用 Lambda 的主動追蹤功能，請參照 AWS 官方文檔進行設置。啟用後，您將能夠在 X-Ray 中看到更詳細的追蹤資訊。
<img src="https://github.com/chenHungTzu/otel_dotnet_lambda_sample/blob/master/doc/setting.png?raw=true" style="zoom:70%;" />


## 結果展示
<img src="https://github.com/chenHungTzu/otel_dotnet_lambda_sample/blob/master/doc/x-ray.png?raw=true" style="zoom:70%;" />

