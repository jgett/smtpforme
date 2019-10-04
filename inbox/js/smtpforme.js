// SmtpForMe user interface
(function($){
    $.fn.smtpforme = function(options){
        return this.each(function(){
            var $this = $(this);

            var opts = $.extend({}, {'host': 'http://127.0.0.1:4856'}, options, $this.data());
            
            var started = false;
            
            var signalrPath = (opts.host.endsWith('/') ? '' : '/') + 'signalr'
            var hubUrl = opts.host + signalrPath;
            
            $.getScript(hubUrl + '/hubs', function(){            
                $.connection.hub.url = hubUrl;

                var message = $.connection.messageHub;

                $this.on('click', '.delete-all', function(e){
                    if (started){
                        message.server.deleteAll();
                    }
                }).on('click', '.delete-message', function(e){
                    if (started){
                        var id = $(this).data('message-id');
                        message.server.deleteMessage(id);
                        $('.message-modal', $this).modal('hide');
                    }
                });


                var createRow = function(msg){
                    console.log(msg);
                    var del = $('<a/>', {'class': 'danger', 'href': '#'}).html('[x]').on('click', function(e){
                        e.preventDefault();
                        message.server.deleteMessage(msg.Id);
                    });

                    var subj = $('<a/>', {'href': '#', 'data-toggle': 'modal', 'data-target': '.message-modal'}).html(msg.Subject);
                    subj.data('msg', msg);

                    return $('<tr/>')
                        .append($('<td/>').html(del))
                        .append($('<td/>').html(moment(new Date(msg.ReceivedOn)).format('YYYY-MM-DD HH:mm:ss')))
                        .append($('<td/>').html(msg.From))
                        .append($('<td/>').html(msg.To))
                        .append($('<td/>').html(subj));
                };

                message.client.refresh = function(messages){
                    var rows = [];

                    $.each(messages, function(index, msg){
                        rows.push(createRow(msg));
                    });

                    $('table > tbody', $this).html(rows);
                };

                message.client.newMessage = function(msg){
                    $('table > tbody', $this).prepend(createRow(msg));
                };

                $('.message-modal', $this).on('show.bs.modal', function(e){
                    var lnk = $(e.relatedTarget);
                    var msg = lnk.data('msg');
                    var modal = $(this);

                    $('[data-property]', modal).each(function(){
                        var property = $(this).data('property');
                        $(this).html(msg[property]);
                    });
                    
                    $(".attachments", modal).html("");
                    
                    if (msg.Attachments.length > 0){
                        var attList = new AttachmentsList(msg.Attachments);
                        for (var x = 0; x < attList.count; x++){
                            var att = attList.item(x);
                            var downloadLink = $("<a/>", {"href": "#", "data-index": x}).html(att.FileName).on("click", function(e){
                                e.preventDefault();
                                var index = $(this).data("index");
                                attList.saveAttachment(index);
                            });
                            $(".attachments", modal).append($("<div/>").html(downloadLink));
                        }
                    }

                    $('.delete-message', modal).data('message-id', msg.Id);
                });

                $.connection.hub.start().done(function(){
                    started = true;
                });
            });
        });
    };
    
    function AttachmentsList(attachments){
        this.count = attachments.length;
        
        this.item = function(index){
            if (index >= attachments.length){
                alert('index out of range');
                return;
            }
            
            return attachments[index];
        }
        
        this.saveAttachment = function(index){
            var att = this.item(index);
            
            var b64Data = att.Data;
            var byteCharacters = atob(b64Data);
            var byteNumbers = new Array(byteCharacters.length);
            
            for (let i = 0; i < byteCharacters.length; i++) {
                byteNumbers[i] = byteCharacters.charCodeAt(i);
            }
            
            var byteArray = new Uint8Array(byteNumbers);
            var blob = new Blob([byteArray], {type: att.ContentType});
            
            // use FileSaver.js plugin
            saveAs(blob, att.FileName);
        }
    }
}(jQuery));