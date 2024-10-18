function getMaxYAxis(data) {
    const maxValue = Math.max(...data); // Find the maximum value in the dataset

    // Determine the Y-axis max based on the maximum value
    if (maxValue < 10) {
        return 10;
    } else if (maxValue < 50) {
        return 50;
    } else if (maxValue < 100) {
        return 100;
    } else if (maxValue < 1000) {
        return 1000;
    } else {
        return Math.ceil(maxValue / 1000) * 1000; // For values larger than 1000
    }
}

function getLast8Days() {
    const dates = [];
    const today = new Date();
    for (let i = 7; i >= 0; i--) {
        const date = new Date(today);
        date.setDate(today.getDate() - i);
        dates.push(date.toLocaleString('default', { month: 'short', day: 'numeric' }));
    }
    return dates;
}

var kasperskyInternetPC = [3, 7, 17, 25, 0, 0, 1, 2];
var v3internetPC = [3, 7, 17, 25, 1, 0, 0, 2];


var devicesByDepartmentData = [0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 9, 0, 0, 0, 0, 1];
const last8DaysLabels = getLast8Days();
var internetPC = new Chart(document.getElementById("internetPC"), {
    type: 'bar',
    data: {
        labels: last8DaysLabels,
        datasets: [
            {
                label: "Kaspersky",
                lineTension: 0.3,
                backgroundColor: "rgb(229 126 85)",
                hoverBackgroundColor: "rgb(229 126 85)",
                borderColor: "rgb(229 126 85)",
                data: kasperskyInternetPC,
                maxBarThickness: 15,
            },
            {
                label: "AhnLab V3",
                lineTension: 0.3,
                backgroundColor: "rgb(0 111 187)",
                hoverBackgroundColor: "rgb(0 111 187)",
                borderColor: "rgb(0 111 187)",
                data: v3internetPC,
                maxBarThickness: 15,
                datalabels: {  // Cấu hình nhãn dữ liệu cho từng cột
                    align: 'end',  // Vị trí nhãn ở cuối mỗi cột
                    anchor: 'start'  // Điểm neo của nhãn là ở điểm đầu của cột
                }
            }
        ],
    },
    options: {
        aspectRatio: 5 / 3,
        responsive: true,
        scales: {
            x: {
                grid: {
                    offset: false
                }
            },
            yAxes: [{
                ticks: {
                    max: getMaxYAxis([Math.max(...kasperskyInternetPC), Math.max(...v3internetPC)])
                }
            }]
        }
    },
});

var kasperskySimaxPC = [395, 881, 1215, 189, 3530, 0, 0, 3506];
var v3SimaxPC = [13638, 12178, 845, 261, 230, 4, 8, 46];
var simaxPC = new Chart(document.getElementById("simaxPC"), {
    type: 'bar',
    data: {
        labels: last8DaysLabels,
        datasets: [
            {
                label: "Kaspersky",
                lineTension: 0.3,
                backgroundColor: "rgb(229 126 85)",
                hoverBackgroundColor: "rgb(229 126 85)",
                borderColor: "rgb(229 126 85)",
                data: kasperskySimaxPC,
                maxBarThickness: 15,
            },
            {
                label: "AhnLab V3",
                lineTension: 0.3,
                backgroundColor: "rgb(0 111 187)",
                hoverBackgroundColor: "rgb(0 111 187)",
                borderColor: "rgb(0 111 187)",
                data: v3SimaxPC,
                maxBarThickness: 15,
                datalabels: {  // Cấu hình nhãn dữ liệu cho từng cột
                    align: 'end',  // Vị trí nhãn ở cuối mỗi cột
                    anchor: 'start'  // Điểm neo của nhãn là ở điểm đầu của cột
                }
            }
        ],
    },
    options: {
        aspectRatio: 5 / 3,
        responsive: true,
        scales: {
            x: {
                grid: {
                    offset: false
                }
            },
            yAxes: [{
                ticks: {
                    max: getMaxYAxis([Math.max(...kasperskySimaxPC), Math.max(...v3SimaxPC)])
                }
            }]
        }
    },
});

var trojanInternetData = [3, 0, 12, 25, 0, 0, 1, 0];
var adwareInternetData = [0, 0, 1, 0, 0, 0, 0, 0];
var virusInternetData = [0, 1, 0, 0, 0, 0, 0, 0];
var maliciousLinkInternetData = [0, 0, 4, 0, 0, 0, 0, 0];
var phishingLinkInternetData = [0, 0, 0, 0, 0, 0, 0, 2];
var internetVirusSpecies = new Chart(document.getElementById("internetVirusSpecies"), {
    type: 'bar',
    data: {
        labels: last8DaysLabels,
        datasets: [
            {
                label: "Trojan",
                lineTension: 0.3,
                backgroundColor: "rgb(229 126 85)",
                hoverBackgroundColor: "rgb(229 126 85)",
                borderColor: "rgb(229 126 85)",
                data: trojanInternetData,
                maxBarThickness: 15,
            },
            {
                label: "Adware",
                lineTension: 0.3,
                backgroundColor: "rgb(255 98 62)",
                hoverBackgroundColor: "rgb(255 98 62)",
                borderColor: "rgb(255 98 62)",
                data: adwareInternetData,
                maxBarThickness: 15,
                
            },
            {
                label: "Virus",
                lineTension: 0.3,
                backgroundColor: "rgb(151 151 165)",
                hoverBackgroundColor: "rgb(151 151 165)",
                borderColor: "rgb(151 151 165)",
                data: virusInternetData,
                maxBarThickness: 15,

            },
            {
                label: "Malicious link",
                lineTension: 0.3,
                backgroundColor: "rgb(255 198 92)",
                hoverBackgroundColor: "rgb(255 198 92)",
                borderColor: "rgb(255 198 92)",
                data: maliciousLinkInternetData,
                maxBarThickness: 15,

            },
            {
                label: "Phishing link",
                lineTension: 0.3,
                backgroundColor: "rgb(119 196 255)",
                hoverBackgroundColor: "rgb(119 196 255)",
                borderColor: "rgb(119 196 255)",
                data: phishingLinkInternetData,
                maxBarThickness: 15,

            }
        ],
    },
    options: {
        aspectRatio: 5 / 3,
        responsive: true,
        scales: {
            x: {
                grid: {
                    offset: false
                }
            },
            yAxes: [{
                ticks: {
                    max: getMaxYAxis([Math.max(...trojanInternetData), Math.max(...adwareInternetData), Math.max(...virusInternetData), Math.max(...maliciousLinkInternetData), Math.max(...phishingLinkInternetData)])
                }
            }]
        }
    },
});

var trojanSimaxData = [1127, 1392, 1744, 311, 0, 3701, 0, 3519];
var adwareSimaxData = [1, 0, 0, 0, 0, 0, 0, 12];
var virusSimaxData = [0, 1, 0, 0, 0, 0, 0, 0];
var win32kashuSimaxData = [12791, 11487, 158, 102, 1, 0, 28, 2];
var malwareSimaxData = [5, 9, 8, 0, 8, 0, 0, 2];
var otherSimaxData = [6, 1, 4, 4, 2, 0, 0, 2];
var simaxVirusSpecies = new Chart(document.getElementById("simaxVirusSpecies"), {
    type: 'bar',
    data: {
        labels: last8DaysLabels,
        datasets: [
            {
                label: "Trojan",
                lineTension: 0.3,
                backgroundColor: "rgb(229 126 85)",
                hoverBackgroundColor: "rgb(229 126 85)",
                borderColor: "rgb(229 126 85)",
                data: trojanInternetData,
                maxBarThickness: 15,
            },
            {
                label: "Adware",
                lineTension: 0.3,
                backgroundColor: "rgb(255 98 62)",
                hoverBackgroundColor: "rgb(255 98 62)",
                borderColor: "rgb(255 98 62)",
                data: adwareInternetData,
                maxBarThickness: 15,

            },
            {
                label: "Virus",
                lineTension: 0.3,
                backgroundColor: "rgb(151 151 165)",
                hoverBackgroundColor: "rgb(151 151 165)",
                borderColor: "rgb(151 151 165)",
                data: virusInternetData,
                maxBarThickness: 15,

            },
            {
                label: "Win32 Kashu.E",
                lineTension: 0.3,
                backgroundColor: "rgb(207 57 23)",
                hoverBackgroundColor: "rgb(207 57 23)",
                borderColor: "rgb(207 57 23)",
                data: win32kashuSimaxData,
                maxBarThickness: 15,

            },
            {
                label: "Malware",
                lineTension: 0.3,
                backgroundColor: "rgb(123 119 219)",
                hoverBackgroundColor: "rgb(123 119 219)",
                borderColor: "rgb(123 119 219)",
                data: malwareSimaxData,
                maxBarThickness: 15,

            },
            {
                label: "Other",
                lineTension: 0.3,
                backgroundColor: "rgb(99 171 35)",
                hoverBackgroundColor: "rgb(99 171 35)",
                borderColor: "rgb(99 171 35)",
                data: otherSimaxData,
                maxBarThickness: 15,

            }
        ],
    },
    options: {
        aspectRatio: 5 / 3,
        responsive: true,
        scales: {
            x: {
                grid: {
                    offset: false
                }
            },
            yAxes: [{
                ticks: {
                    max: getMaxYAxis([Math.max(...trojanSimaxData), Math.max(...adwareSimaxData), Math.max(...virusSimaxData), Math.max(...win32kashuSimaxData), Math.max(...otherSimaxData)])
                }
            }]
        }
    },
});

var devicesByDepartment = new Chart(document.getElementById("devicesByDepartment"), {
    type: 'bar',
    data: {
        labels: ['PLAN', 'ACCOUNT', 'PURCHASE', 'QC', 'IT', 'UTILITY', 'SERCURITY', 'EDU', 'HR', 'PRODUCTION 1', 'PRODUCTION 2', 'ADM', 'MEETING ROOM', 'WAREHOUSE', 'ACC', 'MHA'],
        datasets: [
            {
                label: "Number of devices",
                lineTension: 0.3,
                backgroundColor: "rgb(229 126 85)",
                hoverBackgroundColor: "rgb(229 126 85)",
                borderColor: "rgb(229 126 85)",
                data: devicesByDepartmentData,
                maxBarThickness: 15,
            }
        ],
    },
    options: {
        aspectRatio: 5 / 3,
        responsive: true,
        scales: {
            x: {
                grid: {
                    offset: false
                }
            },
            yAxes: [{
                ticks: {
                    max: getMaxYAxis([...devicesByDepartmentData])
                }
            }]
        }
    },
});