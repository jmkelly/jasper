﻿using Jasper;
using Microsoft.AspNetCore.Hosting;

namespace JasperHttpTesting
{
    public class JasperAlbaUsage : SystemUnderTestBase
    {
        private readonly JasperRuntime _runtime;

        public JasperAlbaUsage(JasperRuntime runtime) : base(null)
        {
            _runtime = runtime;
        }


        protected override IWebHost buildHost()
        {
            return _runtime.Host;
        }
    }
}
