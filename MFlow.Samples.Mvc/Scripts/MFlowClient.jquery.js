﻿(function( $ ){

  $.fn.MFlowClient = function(type, settings) {
  
            var form = this;
               
            var onBlur = function()
            {
                var blured = $(this);
    			var validate = '{';
    
    			form.find('input[id][name]').each(function() {
    				validate += '"'+$(this).attr('id') + '":"' + $(this).val() + '",';
    			});   
    			
    			validate = validate.substring(0, validate.length-1);
    			validate += '}';
    		   
    			var request = '{"validate":'+validate+',"type":"'+type+'"}';
    			
    			$.ajax({
    				type: 'POST',
    				url: settings.validationUrl,
    				contentType: 'application/json;charset=utf-8',
    				data:request,
    				success: function (data) {
    				
    				        blured.removeClass(settings.validationErrorClass);
    				        $('span[data-valmsg-for="' + blured.attr('id') + '"]').addClass(settings.validationValidClass);
        				   
        				   for(var i=0; i<data.length; i++)
        				   {
        					   var members = data[i].MemberNames;
        					   for(var m=0; m<members.length; m++)
        					   {
        						   if(blured.attr('id') == members[m])
        						   {
        							   var memberName = members[m];
        							   $('#'+memberName).addClass(settings.validationErrorClass);
        							   
        							   var errorNode = $('span[data-valmsg-for="' + memberName + '"]');
        							   errorNode.removeClass(settings.validationValidClass);
        							   errorNode.addClass(settings.validationErrorClass);
        							   errorNode.html(data[i].ErrorMessage);
        						   }
        					   }
        				   }
			         }
    			});           	
            }
            
            if(settings.errorOnBlur)
            {
                this.find($('input[id][name]')).blur(onBlur);
            }

            if(settings.hintOnFocus)
            {
                this.find($('input[id][name]')).focus(onBlur);
            }
	};
})( jQuery );