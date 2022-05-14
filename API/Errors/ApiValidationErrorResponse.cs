namespace API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        //error happens due to client error input data 
        //always status code is 400, so no need to take it as a variable parameter 
        //all i need is to add here an additional property to store array of errors, Ienumerbale of strings named errors
        //list of errors that may occur due to inputs validation errors
        // that's why i needed to extend the apiresponse class by this class 
        public ApiValidationErrorResponse() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }

        //it happens due to [apicontroller], which add the validation error to the of model state, which responses to the API server 
        //u need to override and configure the functionality of [apicontroller] in the pipeline  
        //go deep in [apicontroller] and get the list of occured validation errors and list them to an array and fill the Errors property here with this array 

    }
}
