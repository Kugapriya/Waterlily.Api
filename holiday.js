$(function() {
    $('#navbar-placeholder').load('index.html nav');
    $('#sidebar-placeholder').load('index.html #sidebar');
});

$(document).ready(function() {
 
    $('#workingDaysForm').submit(function(event) {
        event.preventDefault(); 
        
       
        const startDate = $('#startDate').val();
        const endDate = $('#endDate').val();

       
        if (new Date(startDate) > new Date(endDate)) {
            alert("Start date cannot be later than end date.");
            return;
        }

        
        $.ajax({
            url: `https://localhost:5148/api/PublicHoliday/CalculateWorkingDays?startDate=${startDate}&endDate=${endDate}`,
            type: 'GET',
            success: function(response) {
               
                $('#result').text(`Working Days: ${response.workingDays}`);
            },
            error: function(xhr, status, error) {
               
                $('#result').text("Error calculating working days.");
                console.error('Error:', error);
            }
        });
    });
});


function loadHolidays() {
    $.ajax({
        url: 'https://localhost:5148/api/PublicHoliday/getAllHolidays', 
        method: 'GET',
        success: function(data) {
            let holidayTableBody = $('#holidayTableBody');
            holidayTableBody.empty();

            const uniqueHolidays = Array.from(new Map(data.map(item => [item.holidayDate + item.reason, item])).values());

            uniqueHolidays.forEach(function(holiday) {
                holidayTableBody.append(`
                    <tr data-id="${holiday.id}">
                        <td>${new Date(holiday.holidayDate).toLocaleDateString()}</td>
                        <td>${holiday.reason}</td>
                        <td>
                            <button class="btn btn-success btn-icon edit-holiday">
                                <i class="fas fa-edit"></i> 
                            </button>
                            <button class="btn btn-danger btn-icon delete-holiday" data-id="${holiday.id}">
                                <i class="fas fa-trash"></i>
                            </button>
                        </td>
                    </tr>
                `);
            });
        },
        error: function(error) {
            console.error('Error loading holidays:', error);
        }
    });
}


$('#holidayForm').submit(function(e) {
    e.preventDefault();
    let date = $('#holidayDate').val();
    let reason = $('#holidayReason').val();

    $.ajax({
        url: 'https://localhost:5148/api/PublicHoliday/insertholiday', 
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            Reason: reason,
            HolidayDate: date
        }),
        success: function() {
            alert('Holiday added successfully');
            loadHolidays(); 
            $('#holidayForm')[0].reset(); 
        },
        error: function(error) {
            console.error('Error adding holiday:', error);
        }
    });
});

$(document).on('click', '.edit-holiday', function() {
    let row = $(this).closest('tr');
    let holidayId = row.data('id');
    let holidayDate = row.find('td').eq(0).text();
    let holidayReason = row.find('td').eq(1).text();

   
    let formattedDate = new Date(holidayDate).toISOString().split('T')[0];

  
    $('#holidayDate').val(formattedDate);
    $('#holidayReason').val(holidayReason);

    
    $('#holidayForm button').text('Update Holiday');

    
    $('#holidayForm').off('submit').on('submit', function(e) {
        e.preventDefault();
        let newReason = $('#holidayReason').val();
        let newDate = $('#holidayDate').val();

        $.ajax({
            url: `https://localhost:5148/api/PublicHoliday/updateHoliday/${holidayId}`,
            method: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify({
                id: holidayId,
                Reason: newReason,
                HolidayDate: newDate
            }),
            success: function() {
                alert('Holiday updated successfully');
                loadHolidays(); 
                $('#holidayForm button').text('Add Holiday'); 
                $('#holidayForm')[0].reset(); 
            },
            error: function(error) {
                console.error('Error updating holiday:', error);
            }
        });
    });
});


$(document).on('click', '.delete-holiday', function() {
    let holidayId = $(this).data('id');
    
    if (confirm('Are you sure you want to delete this holiday?')) {
        $.ajax({
            url: `https://localhost:5148/api/PublicHoliday/deleteHoliday/${holidayId}`,
            method: 'DELETE',
            success: function() {
                alert('Holiday deleted successfully');
                loadHolidays(); 
            },
            error: function(error) {
                console.error('Error deleting holiday:', error);
            }
        });
    }
});


loadHolidays();