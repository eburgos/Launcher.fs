'use strict';
module.exports = {
	'$type': 'bundle',
	'list': [
		{
			'$type': 'bundle',
			'list': [
				{
					'$type': 'object',
					'$subType': 'enumeration',
					'name': 'ItemStatus',
					'properties': {
						'enabled': {
							'description': 'Item is Enabled'
						},
						'disabled': {
							'description': 'Item is Disabled'
						},
						'bypass': {
							'description': 'Item is bypassed. It behaves as thought it runs but it doesn\'t'
						}
					}
				},
				{
					'$type': 'object',
					'$subType': 'enumeration',
					'name': 'InstancesConfig',
					'properties': {
						'singleInstance': {
							'description': 'Only one instance of this process will run'
						},
						'perCpu': {
							'description': 'One instance per cpu will run'
						},
						'fixed': {
							'description': 'A fixed amount of instances will run. This will be specified in the "fixedInstanceAmount" field'
						}
					}
				},
				{
					'$type': 'object',
					'$subType': 'enumeration',
					'name': 'StopAction',
					'properties': {
						'nothing': {
							'description': 'Do nothing. This process is supposed to run and exit'
						},
						'relaunch': {
							'description': 'Relaunch. This process is supposed to be always running. In the event of a stop then try to relaunch it'
						},
						'halt': {
							'description': 'Halt. If this process fails then fail the whole application along with other processes'
						}
					}
				},
				{
					'$type': 'object',
					'$subType': 'enumeration',
					'name': 'StopMethod',
					'properties': {
						'kill': {
							'description': 'Just kill the process'
						},
						'command': {
							'description': 'Run a command to stop it'
						}
					}
				},
				{
					'$type': 'object',
					'name': 'ProcessLaunchArguments',
					'properties': {
						'description': {
							'description': 'Description',
							'type': 'string'
						},
						'workingDirectory': {
							'description': 'Working directory',
							'type': 'string'
						},
						'executablePath': {
							'description': 'Path to the executable to run',
							'type': 'string'
						},
						'arguments': {
							'description': 'Process arguments',
							'type': {
								'namespace': '',
								'name': 'string',
								'modifier': 'list'
							}
						}
					}
				},
				{
					'$type': 'object',
					'name': 'StopConfig',
					'properties': {
						'stopMethod': {
							'description': 'How to stop the process',
							'type': {
								'name': 'StopMethod'
							}
						},
						'stopProcess': {
							'description': 'Process to run if stopMethod is "command"',
							'type': {
								'name': 'ProcessLaunchArguments'
							}
						},
						'onStopAction': {
							'description': 'What to do if the process stops',
							'type': {
								'name': 'StopAction'
							}
						},
						'relaunchTimeThreshold': {
							'description': 'Relaunch time threshold. If the process halts before this amount of milliseconds then halt the application, otherwise relaunch',
							'type': 'int'
						}
					}
				},
				{
					'$type': 'object',
					'name': 'LaunchItem',
					'properties': {
						'description': {
							'description': 'Description',
							'type': 'string'
						},
						'status': {
							'description': 'Enables or Disables items',
							'type': {
								'name': 'ItemStatus'
							}
						},
						'instanceConfig': {
							'description': 'Sets how process instances will run',
							'type': {
								'name': 'InstancesConfig'
							}
						},
						'fixedInstanceAmount': {
							'description': 'When instanceConfig is set to "fixed" this value sets how many instances of this process should run',
							'type': 'int'
						},
						'startProcess': {
							'description': 'Process to run',
							'type': {
								'name': 'ProcessLaunchArguments'
							}
						},
						'stopConfig': {
							'description': 'Configures how to stop and what to do when it does',
							'type': {
								'name': 'StopConfig'
							}
						},
						'dependents': {
							'description': 'Processes that should execute after this one has successfully executed',
							'type': {
								'name': 'LaunchItem',
								'modifier': 'list'
							}
						}
					}
				},
				{
					'$type': 'object',
					'name': 'Application',
					'properties': {
						'name': {
							'description': 'Application name',
							'type': 'string'
						},
						'serviceName': {
							'description': 'Application name',
							'type': 'string'
						},
						'items': {
							'description': 'Processes that should execute',
							'type': {
								'name': 'LaunchItem',
								'modifier': 'list'
							}
						}
					}
				}
			]
		}
	],
	'location': './Launch',
	'namespace': 'LauncherFs',
	'schema': 'http://LauncherFs.com/2014',
	'$target': [{
		location: './Launch',
		language: 'fsharp'
	}]
};
