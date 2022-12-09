# EverFunTest- 長汎面試技術考題成品 孫誠慶

**線上Demo 連結 http://163.44.249.185/Home/Index**

### 使用環境
VPS - Conoha 日本 Server
Server - Linux CentOS 8 + Jexus + Mono 
DB - MariaDB MySql
.Net Framework 4.8 Mvc

### 使用函式庫框架
**Bootstrap**
**JavaScript**
jQuery + SweetAlert2


### 功能技術簡要
前期環境架設已VPS的Linux CentOS為基礎，並安裝Jexus + Mono模擬IIS站台環境，DB安裝MariaDB 建置。
主要已前端非異步AJAX呼叫後端.Net寫好的與DB串接功能傳接JSON格式為本次開發要點。
前端利用Bootstrap作為簡易模板自行刻出。

**1 帳號與電話號碼資料不重複**
兩個資料欄位新增鍵值unique key，約束資料唯一性。
資料送進DB未回傳資料代表帳號與電話號碼資料不是唯一性，並阻擋註冊。

**2 登入後三小時自動登出**
新增ADM_LoginKeys資料表存取專屬登入Key與登入時間和登出時間。
專屬登入Key利用帳號+登入時間轉成MD5亂數，並把Key存.Net Sessionc。
頁面進入時和呼叫更新資料時會先進行Key的判斷，和資料表ADM_LoginKeys比對是否3小時內，
否則自動登出。

** 3 帳號與密碼格式 **
使用前端JavaScript來判斷正規式。

** 4 勾選是否記憶帳號 **
使用前端jQuery Cookie插件存取永久性帳號資料，
頁面載入時第一判斷是否存在Cookie依據。

** 5 登入後資料存取顯示 **
登入後，接收到JSON資料後，並存取至localStorage以利於重整頁面時不必再呼叫DB撈取資料。
抓到登入資訊利用前端直接切換頁面格局顯示會員資料。

** 6 大頭照功能與生日日期選擇 **
大頭照使用前端JavaScript把圖像轉成Base64編碼再丟入DB存取。
生日日期選擇是使用bootstrap-datepicker插件。


