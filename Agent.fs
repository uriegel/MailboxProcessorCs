namespace MailboxProcessorCs

open System

type Agent<'a>(onMsg: Action<'a>) = 
    let agent = MailboxProcessor<'a>.Start(fun agent -> 
        let rec loop () = async {
            let! msg = agent.Receive ()
            onMsg.Invoke msg
            return! loop ()
        }
        loop ()
    )

    member this.Post(msg) = agent.Post msg
  