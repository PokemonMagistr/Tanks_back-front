using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using System.Net;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace BL
{
    public class GamePlay : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            int gameId = dataMap.GetInt("gameId");
            GameLogic.MovePlayer(gameId);
            GameLogic.CheckMove(gameId);
            GameLogic.MoveBullets(gameId);


            return Task.CompletedTask;
        }
    }
}
