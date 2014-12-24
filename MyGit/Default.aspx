<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MyGit._Default" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script src="<%= System.Web.VirtualPathUtility.ToAbsolute("~/ajax/reqwest.js")%>" charset="utf-8" type="text/javascript"></script>

    <script type="text/javascript" >

        $.getJSON = function (u, d, s)
        {
            if (typeof (d) == "function")
            { s = d; d = null; }

            return $.ajax({
                dataType: "json"
                   , url: u
                   , data: d
                   , success: s
                   , error: function (err)
                   {
                       console.log("error $.getJSON");
                       console.log(err);
                   }
            });
        };
        ////////////////
        $.get = function (u, d, s, t)
        {
            if (typeof (d) == "function")
            { t = s; s = d; d = null; }

            return $.ajax({
                url: u
              , data: d
              , success: s
              , error: function (err)
              {
                  console.log("error $.get");
                  console.log(err);
              }
              , dataType: t
            });
        };
        /////////
        $.post = function (u, d, s, t)
        {
            if (typeof (d) == "function")
            { t = s; s = d; d = null; }

            return $.ajax({
                type: "POST"
              , url: u
              , data: d
              , success: s
              , error: function (err)
              {
                  console.log("error $.post");
                  console.log(err);
              }
              , dataType: t
            });
        };
        // 

        $.jsonp = function (u, params, cb, cbn, fn)
        {
            var sep = "?", ps = "";
            if (params != null)
            {
                if (u !== null && u.indexOf("?") !== -1) sep = "&";
                for (var key in params)
                {
                    // important check that this is objects own property
                    // not from prototype prop inherited
                    if (params.hasOwnProperty(key))
                    {
                        ps += sep + encodeURIComponent(key) + "=" + encodeURIComponent(params[key]);
                        sep = "&";
                    }
                } // Next key
            } // End if(params != null)

            return $.ajax({
                url: (u + ps)
              , type: 'jsonp'
              , jsonpCallback: cb
              , jsonpCallbackName: cbn
             , success: fn
                //,error : function(err) { console.log("error $.jsonp");console.log(err);}
            })
        };


        function InstantSearch()
        {
            window.InstaRequests = [];
            var obj = document.getElementById("txtSearchText");

            // name of event different, e.g. click vs. onclick
            if (obj.addEventListener)
            {
                // param3: boolean - bubbling/capturing
                // http://www.w3schools.com/jsref/met_document_addeventlistener.asp
                obj.addEventListener("keyup", function (e)
                {
                    // e.preventDefault();

                    if (window.InstaRequests[this.id])
                    {
                        window.InstaRequests[this.id].abort();
                    }

                    window.InstaRequests[this.id] = $.getJSON("<%= System.Web.VirtualPathUtility.ToAbsolute("~/Blog/Search")%>", { q: obj.value }, function (data)
                    {
                        console.log("getJSON: data:");
                        console.log(data);
                        // obj.value = "Hello World";
                        // obj.value = "my" + data.searched_for;
                        console.log("insta:");
                        var div = document.getElementById("divSR"); //.parentNode;
                        //console.log(div);

                        // http://stackoverflow.com/questions/3955229/remove-all-child-elements-of-a-dom-node-in-javascript
                        // div.innerHTML = "";
                        // Much faster than innerHTML
                        //while (div.firstChild) div.removeChild(div.firstChild);
                        var last; while (last = div.lastChild) div.removeChild(last);

                        // beforebegin
                        // afterbegin
                        // beforeend
                        // afterend
                        div.insertAdjacentHTML('beforeend', '<div style="background-color: hotpink; width: 200px; height: 200px;">' + data.searched_for + '</div>');

                    }).request;
                    console.log(InstaRequests[this.id]);

                });
            }
            else if (obj.attachEvent)
            {
                // IE 8
                // obj.attachEvent("onclick", setCheckedValues);
            }
            else
                console.log("no event listener");
        }


    </script>

</head>
<body>
    <form id="form1" runat="server">

        <table id="tblTest">
        </table>

        <div id="result"></div>

    </form>

    <script type="text/javascript">


        function onDocumentReady()
        {
            /*
            $.get("<%= System.Web.VirtualPathUtility.ToAbsolute("~/ajax/RepoContent.ashx")%>", function (data)
            {
                var res = document.getElementById("result");

                var last;
                while (last = res.lastChild) res.removeChild(last);



                // res.innerHTML = data.responseText;
                var tn = document.createTextNode(data.responseText);
                res.appendChild(tn);
                // res.innerHTML = res.innerHTML.replace("\n", "<br />");
                res.innerHTML = res.innerHTML
                                        .replace(/ /g, '\u00a0')
                                        .replace(/(?:\r\n|\r|\n)/g, '<br />')
                ;

                // $(".result").html(data);
                // alert("Load was performed.");
            });
            */


            $.post("<%= System.Web.VirtualPathUtility.ToAbsolute("~/ajax/RepoContent.ashx")%>", function (data)
            {
                console.log("post");
                var res = document.getElementById("result");

                var last;
                while (last = res.lastChild) res.removeChild(last);

                // res.innerHTML = data.responseText;

                var tn = document.createTextNode(data.responseText);
                res.appendChild(tn);
                // res.innerHTML = res.innerHTML.replace("\n", "<br />");
                res.innerHTML = res.innerHTML
                                        .replace(/ /g, '\u00a0')
                                        .replace(/(?:\r\n|\r|\n)/g, '<br />')
                ;


                var d1 = document.getElementById('tblTest');
                

                var obj = JSON.parse(data.responseText);
                console.log(obj);

                var sb = [];

                console.log(obj);

                for (var i = 0; i < obj.length; ++i)
                {
                    if (obj[i].isDirectory)
                        sb.push('<tr><td><img width="16px" height="16px" src="<%= System.Web.VirtualPathUtility.ToAbsolute("~/images/brightfolder.svg")%>" /></td><td>' + obj[i].Name + '</td></tr>');
                        
                }

                var str = sb.join("");
                d1.insertAdjacentHTML('beforeend', str);

                // $(".result").html(data);
                // alert("Load was performed.");
            });

        }

        onDocumentReady();
    </script>

    <img style="width: 5%; height: 5%;" src="/images/brightfolder.svg" alt="bright folder icon" />
   
    Internet exploer is the only browser that needs viewBox on the svg element to scale 
       viewBox="0 0 375 650"
   
</body>
</html>
