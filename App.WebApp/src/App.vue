<script setup lang="ts">
import bootstrap from 'bootstrap/scss/bootstrap.scss'
</script>

<template>
  <body>
    <div class="formal-form">
      <div class="fisrtData">
        <b>NHẬT KÝ CÔNG VIỆC BỘ PHẬN IT</b>
        <div class="table-view-1">
          <tl>
            <th>Num 1</th>
            <th>Num 2</th>
            <th>Num 3</th>
            <th>Num 4</th>
          </tl>
        </div>
        <div class="table-view-2">
          <tl>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
        </div>
      </div>
      <div class="NextData1">
        <p>
          Ngày tháng:
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;/2024&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
        <p>
          / Ca làm&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;/Ca
          ngày&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
      </div>
      <div class="NextData2">
        <p>&nbsp;&nbsp;1. Network requiment check</p>
      </div>
      <div class="NextData3">
        <p>Thời gian kiểm tra /</p>
        <tl>
          <td>7:00</td>
          <td>9:00</td>
          <td>11:00</td>
          <td>13:00</td>
          <td>15:00</td>
          <td>17:00</td>
        </tl>
      </div>
      <div class="NextData4">
        <div class="dup-col">
          <p>Phân loại</p>
          <p>Model</p>
        </div>
        <div class="dup-col">&nbsp;</div>
      </div>
      <div class="NextData5">
        <div class="dup-col">
          <tl>
            <th :colspan="6">NETWORK</th>
            <th>Internet</th>
            <td>Router-1/Router-2</td>
            <td colspan="2">Firewall-1/Firewall-2</td>
            <th>SIMAX</th>
            <td>NMS FMS NVR WLC UPS</td>
            <td>IPScan Firewall</td>
            <td>Router-1</td>
            <td>Router-2</td>
            <td>SIMAX_WAN_SW1</td>
            <td>SIMAX_WAN_SW2</td>
            <td>SIMAX_CORE1</td>
            <td>SIMAX_CORE2</td>
            <td>ISE#1 ISE#2</td>
          </tl>
        </div>
        <div class="dup-col">
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
        </div>
      </div>
      <div class="NextData6">
        <p>&nbsp;&nbsp;2. Server check</p>
      </div>
      <div class="NextData3">
        <p>Thời gian kiểm tra /</p>
        <tl>
          <td>7:00</td>
          <td>9:00</td>
          <td>11:00</td>
          <td>13:00</td>
          <td>15:00</td>
          <td>17:00</td>
        </tl>
      </div>
      <div class="NextData4">
        <div class="dup-col">
          <p>Phân loại</p>
          <p>Model</p>
        </div>
        <div class="dup-col">&nbsp;</div>
      </div>
      <div class="NextData7">
        <div class="dup-col">
          <tl>
            <th :colspan="6">SERVER</th>
            <th>VM</th>
            <td>VM ESXI</td>
            <th>SIMAX</th>
            <td>DB-P</td>
            <td>DB-S</td>
            <td>Application-P</td>
            <td>Application-S</td>
            <td>MSG-P</td>
            <td>MSG-S</td>
            <td>Backuper</td>
            <td>Tap Library</td>
            <th>Oracle</th>
            <td>Ora-arc 1 (Over70%)</td>
            <td>Ora-arc 2 (Over70%)</td>
            <td>Oradump (Over70%)</td>
            <th>Other</th>
            <td>EES 0.5</td>
          </tl>
        </div>
        <div class="dup-col">
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
          <tl class="tab-warning">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tl>
        </div>
      </div>
      <div class="NextData6">
        <p>&nbsp;&nbsp;3. Temperature and humidity status</p>
      </div>
      <div class="NextData3">
        <p>Thời gian kiểm tra /</p>
        <tl>
          <td>7:00</td>
          <td>9:00</td>
          <td>11:00</td>
          <td>13:00</td>
          <td>15:00</td>
          <td>17:00</td>
        </tl>
      </div>
      <div class="NextData8">
        <p style="border-top: 0px">Humidity / Độ ẩm (Over 57%)</p>
        <tl>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tl>
      </div>
      <div class="NextData8">
        <p>Temperature / Nhiệt độ (Over 25*C)</p>
        <tl>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tl>
      </div>
      <div class="NextData6">
        <p>&nbsp;&nbsp;4, Log check</p>
      </div>
      <div class="NextData3">
        <p>Thời gian kiểm tra /</p>
        <tl>
          <td>7:00</td>
          <td>9:00</td>
          <td>11:00</td>
          <td>13:00</td>
          <td>15:00</td>
          <td>17:00</td>
        </tl>
      </div>
      <div class="NextData8">
        <p style="border-top: 0px">Archive Log (Free) (Under 40)</p>
        <tl>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tl>
      </div>
      <div class="NextData8">
        <p>Users_O (Used) (Over 70)</p>
        <tl>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tl>
      </div>
      <div class="NextData6">
        <p>&nbsp;&nbsp;5, Monitoring refresh</p>
        <p>Check Monitoring phòng bác Tổng</p>
      </div>
      <div class="NextData6">
        <p>&nbsp;&nbsp;Các công việc khác /</p>
      </div>
      <div class="NextData9">
        <div class="col-num">
          <p>1</p>
        </div>
      </div>
      <div class="NextData9">
        <div class="col-num">
          <p>&nbsp;</p>
        </div>
      </div>
      <div class="NextData9">
        <div class="col-num">
          <p>2</p>
        </div>
      </div>
      <div class="NextData9">
        <div class="col-num">
          <p>&nbsp;</p>
        </div>
      </div>
      <div class="NextData9">
        <div class="col-num">
          <p>3</p>
        </div>
      </div>
      <div class="NextData9">
        <div class="col-num">
          <p>&nbsp;</p>
        </div>
      </div>
      <div class="NextData9">
        <div class="col-num">
          <p>4</p>
        </div>
      </div>
      <div class="NextData9">
        <div class="col-num">
          <p>&nbsp;</p>
        </div>
      </div>
    </div>
  </body>
</template>

<style scoped>
.formal-form {
  border: 2px solid rgb(0, 0, 0);
  margin: 60px 0;
}
.formal-form p {
  float: left;
}
.fisrtData {
  width: 100%;
  position: relative;
  margin: auto;
}
.fisrtData b {
  float: left;
  font-weight: bold;
  padding: 20px 70px;
}
.table-view-1 {
}
.table-view-2 {
}
.table-view-1 th:first-child {
  border-left: 1px solid rgb(0, 0, 0);
}
.table-view-1 th {
  border-right: 1px solid rgb(0, 0, 0);
  border-bottom: 1px solid rgb(0, 0, 0);
  padding: 0 25px;
}
.table-view-1 th:last-child {
  border-right: 0px;
}
.formal-form {
  width: 768px;
}
.table-view-2 td:first-child {
  border-left: 1px solid rgb(0, 0, 0);
}
.table-view-2 td {
  border-right: 1px solid rgb(0, 0, 0);
  border-bottom: 1px solid rgb(0, 0, 0);
  padding: 10px 45.5px;
}
.table-view-2 td:last-child {
  border-right: 0px;
}
.NextData1 p:first-child {
  margin: 10px 0 0 0;
  border-top: 2px solid rgb(0, 0, 0);
  border-bottom: 2px solid rgb(0, 0, 0);
  border-right: 2px solid rgb(0, 0, 0);
}
.NextData1 p:last-child {
  margin: 10px 0 0 0;
  border-top: 2px solid rgb(0, 0, 0);
  border-bottom: 2px solid rgb(0, 0, 0);
}
.NextData1 p {
  float: left;
  width: 50%;
}
.NextData2 {
  float: left;
  width: 100%;
  text-align: left;
}
.NextData3 p {
  width: 50%;
  border-top: 1px solid rgb(0, 0, 0);
  border-bottom: 1px solid rgb(0, 0, 0);
  border-right: 1px solid rgb(0, 0, 0);
}
.NextData3 td:first-child {
  border-left: 0px;
}
.NextData3 td {
  padding: 0 14.966px;
  border: 1px solid rgb(0, 0, 0);
  border-right: 0px;
}
.NextData4 {
  width: 100%;
  float: left;
  border-bottom: 1px solid rgb(0, 0, 0);
}
.NextData4 .dup-col {
  width: 50%;
  float: left;
  border-right: 1px solid rgb(0, 0, 0);
}
.NextData4 .dup-col:last-child {
  border-right: 0px;
}
.NextData4 .dup-col p {
  width: 50%;
  border-right: 1px solid rgb(0, 0, 0);
}
.NextData4 .dup-col p:last-child {
  border-right: 0px;
}
.NextData5 .dup-col {
  width: 50%;
  float: left;
}
.NextData5 .dup-col th {
  width: calc(25% - 1px);
  float: left;
}
.NextData5 .dup-col th:first-child {
  height: 297px;
  border-right: 1px solid rgb(0, 0, 0);
  padding-top: 140px;
}
.NextData5 .dup-col th:nth-child(2) {
  height: 54px;
  padding-top: 13px;
  border-bottom: 1px solid rgb(0, 0, 0);
}
.NextData5 .dup-col th:nth-child(5) {
  height: 240px;
  padding-top: 108px;
}
.NextData5 .dup-col .tab-warning td {
  width: auto;
  padding: 1px 29.28px;
  border-left: 0px;
}
.NextData5 .dup-col .tab-warning td:last-child {
  border-right: 0px;
}
.NextData5 .dup-col td {
  width: calc(50% + 2px);
  float: left;
  border-right: 1px solid rgb(0, 0, 0);
  border-bottom: 1px solid rgb(0, 0, 0);
  border-left: 1px solid rgb(0, 0, 0);
}
.NextData6 {
  float: left;
  width: 100%;
  text-align: left;
  border-top: 1px solid rgb(0, 0, 0);
}
.NextData7 .dup-col {
  width: 50%;
  float: left;
}
.NextData7 .dup-col th {
  width: calc(25% - 1px);
  float: left;
}
.NextData7 .dup-col th:first-child {
  height: 351px;
  border-right: 1px solid rgb(0, 0, 0);
  padding-top: 164px;
}
.NextData7 .dup-col th:nth-child(2) {
  border-bottom: 1px solid rgb(0, 0, 0);
}
.NextData7 .dup-col th:nth-child(4) {
  height: 216px;
  padding-top: 96px;
  border-bottom: 1px solid rgb(0, 0, 0);
}
.NextData7 .dup-col th:nth-child(13) {
  height: 81px;
  border-bottom: 1px solid rgb(0, 0, 0);
}
.NextData7 .dup-col .tab-warning td {
  width: auto;
  padding: 1px 29.28px;
  border-left: 0px;
}
.NextData7 .dup-col .tab-warning td:last-child {
  border-right: 0px;
}
.NextData7 .dup-col td {
  width: calc(50% + 2px);
  float: left;
  border-right: 1px solid rgb(0, 0, 0);
  border-bottom: 1px solid rgb(0, 0, 0);
  border-left: 1px solid rgb(0, 0, 0);
}
.NextData8 p {
  width: 50%;
  border-top: 1px solid rgb(0, 0, 0);
  border-right: 1px solid rgb(0, 0, 0);
  padding: 1.3px;
}
.NextData8 td:first-child {
  border-left: 0px;
}
.NextData8 td {
  padding: 1px 17px;
  border: 1px solid rgb(0, 0, 0);
  border-right: 0px;
}
.NextData9 {
  float: left;
  width: 100%;
  text-align: left;
  border-top: 1px solid rgb(0, 0, 0);
}
.NextData9 .col-num {
  width: 16%;
  float: left;
  text-align: center;
  padding-left: 50px;
  border-right: 1px solid;
}
</style>
