const apiUrl="https://localhost:44396/api/ProductAPI/AllProductField"; 
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
            console.log("hello");

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
