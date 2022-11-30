const apiUrl="https://localhost:44396/api/ProductAPI/AllProductField"; 
const receiveUrl="https://localhost:44396/api/ProductAPI/StoreProduct";

    async function fetchPost(){
        try{
        const response= await fetch(`${apiUrl}`);

        if(!response.ok){
            throw new Error(`Failed to Fetch Response ${response.status}`);
        }
        return response.json();
        }catch(e){
            console.log(e);
        }
    }


    async function returnResponse(){
        console.log(fetchPost);
    }


    function listPosts(postContainerElementId){
            

        const postContainerElement= document.getElementById(postContainerElementId);
        if(!postContainerElement){
            return;
        }

        fetchPost()
        .then((posts) => {

            if(!posts){
                postContainerElement.innerHTML="No posts";
                return;
            }
            
            for (var i=1; i<posts.length;i++){

                postContainerElement.appendChild(postElement(posts[i]));
                
            }
            //buttonCreationCanbeDynamic
            var button=document.createElement("button");
            var text=document.createTextNode("Submit");
            button.appendChild(button.appendChild(text));
            postContainerElement.append(button);

           //console.log("Hle");
        })
        .catch((e) => {
            console.log(e);
        });
}

    function postElement(post){

        var inputField = document.createElement("INPUT");
        inputField.setAttribute("type", "text");
        inputField.setAttribute("placeholder", String(post));
        inputField.setAttribute("id",String(post));
        inputField.setAttribute("Name", String(post));


        inputField.style.marginBottom="20px";
        inputField.style.marginLeft="20px";

        var newlabel = document.createElement("Label");
        newlabel.setAttribute("for",inputField.id);
        newlabel.innerHTML = inputField.id;



        var linebreak=document.createElement("BR");

        newlabel.appendChild(inputField);
        newlabel.appendChild(linebreak);
        
        return newlabel;

    }



    
    function handleSubmit(event) {
        event.preventDefault();
    
        const data = new FormData(event.target); //catching form data
    
        const value = Object.fromEntries(data.entries()); //json format
    
        console.log({ value });

        //  var datas = {};
        //  datas.portfolioId = '200';
        //  datas.nodedates = '2000';
    


        $.ajax({
            type: "POST",
            url: "https://localhost:44396/api/ProductAPI/StoreProduct",
            dataType: "json",
            data: JSON.stringify(value),
            contentType: "application/json; charset=utf-8",

            
           
            error: function (error) {
                    alert("Didn't Work");
            }
    });

      }
    
      const form = document.querySelector('form');
      form.addEventListener('submit', handleSubmit);