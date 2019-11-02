
//$(document).ready(function () {
//    $('#nestable').nestable({
//        onDragFinished: function (currentNode, parentNode)  //parentId ve childId buradan geliyor.
//        {
           
//        }

//    });
//});
//   $(document).ready(function () {
//        var moveEl = { from: "", to: "" };
//        var hovering = "";
//        var PreventOnloadSave = 0;
//        var flag = 0;
//       var draggedID;
//       var parentID;

//        var updateOutput = function (e) {

//            var list = e.length ? e : $(e.target),
//                output = list.data('output');

//            var previousVal = output.val();
//            var newVal = window.JSON.stringify(list.nestable('serialize'));
//            console.log ("New value: " + newVal  );

//            if (PreventOnloadSave > 1) {
//                if (previousVal != newVal) {


//                    if (flag == 0) {
//                        if (moveEl.from != moveEl.to) {
//                            console.log(" --> Call the ajax function.");                       
//                            console.log("dragggedID: " + draggedID);
//                            //console.log("DB SLOT: " + moveEl.from);
//                        } else {
//                            console.log("Order changed in the " + moveEl.from + ".");
//                        }
//                    } else {
//                        //console.log("DB SLOT: " + moveEl.to);
//                    }
//                    //console.log("DATA: " + newVal );
//                    //console.log("");
//                    flag++;

//                    // Reset flag and moveEl from/to object
//                    if (flag > 1 || moveEl.from == moveEl.to) {
//                        flag = 0;
//                        moveEl = { from: "", to: "" };
//                    }

//                } else {
//                    console.log("No change, no action!");
//                }
//            } else {
//                PreventOnloadSave++;
//            }

//            if (window.JSON) {
//                output.val(newVal);
//            } else {
//                output.val('JSON browser support required for this demo.');
//            }


//        };

//        // activate Nestable for list 1
//        $('#nestable').nestable({
//            group: 1,
//            maxDepth: 7
//        }).on('change', updateOutput);
//        // output initial serialised data
//        updateOutput($('#nestable').data('output', $('#nestable-output')));

//        //Socialite.load();


//        //-------------------------------------------------
//        $(".dd").on("mouseover", function () {
//            //console.log("mouseover1");
//            hovering = $(this).attr("id");
//        });


//        $(".dd").on("mousedown", function () {
//            console.log("");
//            console.log("--------------------------- Mousedown on an element");

//            setTimeout(function () {
//                if ($("body").find(".dd-dragel")) {
//                    draggedID = $("body").find(".dd-dragel").find(".dd-item").attr("data-id");
//                    parentID = $("body").find(".dd-dragel").find(".dd-list").attr("data-id");

//                    $.ajax({
//                        method: "POST",
//                        url: "/Admin/Pages/List",
//                        data: {
//                            id: draggedID,
//                            parentID:parentID
//                        }
//                    }).fail(function (jqXHR, textStatus, errorThrown) {
//                        alert("Unable to save new list order: " + errorThrown);
//                    });

//                    //console.log( $("body").find(".dd-dragel").html() );
//                    //console.log( $("body").find(".dd-dragel").find(".dd-item").length );
//                    if ($("body").find(".dd-dragel").find(".dd-item").length > 1) {
//                        console.log("Dragged element is red... No can do since maxDepth is 1.");
//                        $("body").find(".dd-dragel").css("background-color", "red");
//                    }
//                }
//            }, 100);

//            console.log("FROM: " + hovering);
//            moveEl.from = hovering;
//        });

//        $(".dd").on("mouseup", function () {
//            console.log("TO: " + hovering);
//            console.log("");
//            moveEl.to = hovering;
//        });
//    });


//$(document).ready(function () {
//    var updateOutput = function (e) {
//        var list = e.length ? e : $(e.target), output = list.data('output');
//        if (window.JSON) {
//            output.val(window.JSON.stringify(list.nestable('serialize')));//, null, 2));
//        } else {
//            output.val('JSON browser support required for this demo.');
//        }
//        console.log(list.nestable('serialize'));
//        console.log(window.JSON.stringify(list.nestable('serialize')));
//    };
//    $('#nestable').nestable({
//        group: 1,
//        maxDepth: 7
//    }).on('change', updateOutput);
//    updateOutput($('#nestable').data('output', $('#nestable-output')));
//});


