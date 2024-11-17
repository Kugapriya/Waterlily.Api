$(document).ready(function () {
    let editingEmployeeId = null; 

    loadEmployees();

 
    function loadEmployees() {
        $.ajax({
            url: 'https://localhost:5148/api/Employee/getAllEmployees',
            method: 'GET',
            success: function (data) {
                const employeeTableBody = $('#employeeTableBody');
                employeeTableBody.empty();

                if (Array.isArray(data) && data.length > 0) {
                    data.forEach(employee => {
                        employeeTableBody.append(`
                            <tr>
                                <td>${employee.name}</td>
                                <td>${employee.email}</td>
                                <td>${employee.jobPosition}</td>
                                <td class="text-center">
                               
                                <button class="btn btn-success btn-icon edit-employee" data-id="${employee.id}" 
                                title="Edit">
                                <i class="fas fa-pen"></i> 
                                </button>


                                <button class="btn btn-danger btn-icon delete-employee" data-id="${employee.id}" 
                                title="Delete">
                                <i class="fas fa-trash"></i>
                                </button>

                                </td>
                            </tr>
                        `);
                    });
                } else {
                    employeeTableBody.append('<tr><td colspan="4" class="text-center">No employees found.</td></tr>');
                }
            },
            error: function (xhr) {
                console.error('Error loading employees:', xhr.responseText);
                alert('Failed to load employees.');
            }
        });
    }


    function insertEmployee(name, email, role) {
        $.ajax({
            url: 'https://localhost:5148/api/Employee/insertEmployee',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ Name: name, Email: email, JobPosition: role }),
            success: function () {
                alert('Employee added successfully');
                $('#employeeForm')[0].reset();
                loadEmployees(); 
            },
            error: function (xhr) {
                console.error('Error adding employee:', xhr.responseText);
                alert('Failed to add employee.');
            }
        });
    }

  
    function updateEmployee(employeeId, name, email, role) {
        $.ajax({
            url: `https://localhost:5148/api/Employee/updateEmployee/${employeeId}`,
            method: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify({ Id: employeeId, Name: name, Email: email, JobPosition: role }),
            success: function () {
                alert('Employee updated successfully');
                editingEmployeeId = null; 
                $('#employeeForm')[0].reset(); 
                loadEmployees(); 
            },
            error: function (xhr) {
                console.error('Error updating employee:', xhr.responseText);
                alert('Failed to update employee.');
            }
        });
    }

   
    $('#employeeForm').submit(function (e) {
        e.preventDefault();
        const name = $('#employeeName').val();
        const email = $('#employeeEmail').val();
        const role = $('#employeeRole').val();

        if (editingEmployeeId) {
            updateEmployee(editingEmployeeId, name, email, role);
        } else {
            insertEmployee(name, email, role);
        }
    });

    $(document).on('click', '.delete-employee', function () {
        const employeeId = $(this).data('id');
        if (confirm('Are you sure you want to delete this employee?')) {
            $.ajax({
                url: `https://localhost:5148/api/Employee/deleteEmployee/${employeeId}`,
                method: 'DELETE',
                success: function () {
                    alert('Employee deleted successfully');
                    loadEmployees();
                },
                error: function (xhr) {
                    console.error('Error deleting employee:', xhr.responseText);
                    alert('Failed to delete employee.');
                }
            });
        }
    });

  
    $(document).on('click', '.edit-employee', function () {
        editingEmployeeId = $(this).data('id');
        $.ajax({
            url: `https://localhost:5148/api/Employee/getEmployeeById/${editingEmployeeId}`,
            method: 'GET',
            success: function (employee) {
                $('#employeeName').val(employee.name);
                $('#employeeEmail').val(employee.email);
                $('#employeeRole').val(employee.jobPosition);
            },
            error: function (xhr) {
                console.error('Error fetching employee details:', xhr.responseText);
                alert('Failed to fetch employee details.');
            }
        });
    });
});
