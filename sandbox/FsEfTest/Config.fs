namespace FsEfTest

module Config =
    open System
    open dotenv.net

    let private envVars = DotEnv.Read();

    let getEnvVar variable = envVars.Item(variable)
        
