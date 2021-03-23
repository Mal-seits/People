$(() => {
    function FillTable() {
        $("tbody").empty();
        $.get('/home/GetAllPeople', function (people) {
            people.forEach(p => {

                $("tbody").append(`
<tr>
    <td>
        ${p.firstName}
    </td>
    <td>
        ${p.lastName}
    </td>
     <td>
        ${p.age}
    </td>
    <td>
        <button class="btn btn-outline-info btn-block" id="edit" data-id=${p.id} data-first-name=${p.firstName} data-last-name=${p.lastName} data-age=${p.age}>Edit</button>
    </td>
    <td>
     <button class="btn btn-outline-danger btn-block" id="delete" data-id=${p.id}>Delete</button>
    </td>
</tr>`);
            });
        });

    }
    FillTable();
    $("#add-btn").on('click', function () {
      
        let firstName = $("#first-name").val();
        let lastName = $("#last-name").val();
        let ageString = $("#age").val();
        let age = parseInt(ageString);
        $("#first-name").val("");
        $("#last-name").val("");
        $("#age").val("");
        $.post("/home/AddPerson", { firstName, lastName, age }, function (person) {
            FillTable();
        })
    })
    $("tbody").on('click', '#edit', function () {
        console.log("here");
        let firstName = $(this).data('first-name');
        let lastName = $(this).data('last-name');
        let age = $(this).data('age');
        let id = $(this).data('id');
        $("#first-name-edit").val(firstName);
        $("#last-name-edit").val(lastName);
        $("#age-edit").val(age);
        $("#id-edit").val(id);
        $('.modal').modal();
    })
    $("#save-btn").on('click', function () {
        console.log("here");
        let firstName = $("#first-name-edit").val();
        let lastName = $("#last-name-edit").val();
        let age = $("#age-edit").val();
        let id = $("#id-edit").val();
        $.post("/home/EditPerson", { firstName, lastName, age, id }, function (p) {
            FillTable();
        })
        $('.modal').modal('hide');
    })
    $("tbody").on('click', '#delete', function () {
        let id = $(this).data('id');
        $.post("home/deletePerson", { id }, function (id) {
            FillTable();
        })
    })
})