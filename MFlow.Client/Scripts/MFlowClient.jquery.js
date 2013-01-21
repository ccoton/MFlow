(function( $ ){

  $.fn.MFlowClient = function(type, settings) {
  
            var form = this;
               
            var onBlur = function(blured, serviceUrl, errorClass, validClass, removeClass)
            {
    			var validate = '{';
    
    			form.find('input[id][name]').each(function() {
    				validate += '"'+$(this).attr('id') + '":"' + $(this).val() + '",';
    			});   
    			
    			validate = validate.substring(0, validate.length-1);
    			validate += '}';
    		   
    			var request = '{"validate":'+validate+',"type":"'+type+'"}';
    			
    			$.ajax({
    				type: 'POST',
    				url: serviceUrl,
    				contentType: 'application/json;charset=utf-8',
    				data:request,
    				success: function (data) {

    				        blured.removeClass(errorClass);
    				        $('span[data-valmsg-for="' + blured.attr('id') + '"]').addClass(validClass);
        				   
        				   if(removeClass != undefined && removeClass != null)
        				   {
	    				        blured.removeClass(removeClass);
    				            $('span[data-valmsg-for="' + blured.attr('id') + '"]').removeClass(removeClass);		   
        				   }
        				   
        				   for(var i=0; i<data.length; i++)
                           {
        					   var members = data[i].MemberNames;
        					   for(var m=0; m<members.length; m++)
        					   {
        						   if(blured.attr('id') == members[m])
        						   {
        							   var memberName = members[m];
        							   $('#'+memberName).addClass(errorClass);
        							   
        							   var errorNode = $('span[data-valmsg-for="' + memberName + '"]');
        							   errorNode.removeClass(validClass);
        							   errorNode.addClass(errorClass);
        							   errorNode.html(data[i].ErrorMessage);
        						   }
        					   }
        				   }
			         }
    			});           	
            }
            
            if(settings.validateOnBlur)
            {
                this.find($('input[id][name]')).blur(function() {
                	onBlur($(this), settings.validationUrl, settings.validationErrorClass, settings.validationValidClass); 
                });
            }

            if(settings.suggestOnFocus)
            {
                this.find($('input[id][name]')).focus(function() {
                	onBlur($(this), settings.suggestionUrl, settings.suggestionClass, settings.validationValidClass, settings.validationErrorClass); 
            	});
            }
	};
})( jQuery );