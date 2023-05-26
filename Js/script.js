$(document).ready(function () {
  fetch('http://localhost:5555/api/data?query=0')
    .then(response => response.json())
    .then(data => {
      var dataTable = $('#data-table').DataTable({
        data: data,
        columns: generateColumns(data),
      });
    })
    .catch((e) => {
      console.log('Error:', e);
    });
});

function generateColumns(data) {
  var columns = [];
  var headersRow = $('#table-headers');

  Object.keys(data[0]).forEach(function (columnName) {
    columns.push({ data: columnName });
    headersRow.append('<th>' + columnName + '</th>');
  });

  return columns;
}