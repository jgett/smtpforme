// SmtpForMe user interface
(function($){
	$.fn.smtpforme = function(options){
		return this.each(function(){
			var $this = $(this);

			var started = false;
			
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
				
				$('.delete-message', modal).data('message-id', msg.Id);
			});
			
			$.connection.hub.start().done(function(){
				started = true;
			});
		});
	};
}(jQuery));
